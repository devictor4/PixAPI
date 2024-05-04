using PixAPI.Business.DTOs;
using PixAPI.Business.Exceptions;
using PixAPI.Repository.Context;
using PixAPI.Repository.Entities;
using static PixAPI.Business.Util.Enumerators;

namespace PixAPI.Business.Services
{
    public class UsuarioService
    {
        private readonly PixAPIContext _pixAPIContext;

        public UsuarioService(PixAPIContext pixAPIContext)
        {
            _pixAPIContext = pixAPIContext;
        }

        public List<UsuarioDTO>? Listar()
        {
            List<UsuarioDTO>? usuarios = new();
            try
            {
                usuarios = _pixAPIContext.Usuario
                    .Select(e => new UsuarioDTO(e)).ToList();

                if(!usuarios.Any()) 
                    throw new BadRequestException("Não foram encontrados usuários.");
            } 
            catch (Exception e)
            {
                throw;
            }

            return usuarios;
        }

        public UsuarioDTO? BuscarPeloDocumento(TipoDocumento tipoDocumento, long documento) =>
            _pixAPIContext.Usuario.Where(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                && e.documento == documento
                && (!e.isExcluido))
            .Select(e => new UsuarioDTO(e)).FirstOrDefault();

        public void CadastrarOuAtualizar(UsuarioDTO? usuarioDTO)
        {
            try
            {
                if ((!usuarioDTO.TipoDocumento.HasValue
                    && !usuarioDTO.Documento.HasValue)
                    || usuarioDTO.Documento == 0) throw new BadRequestException("Necessário informar o documento.");

                Usuario? usuario = _pixAPIContext.Usuario
                    .FirstOrDefault(e => e.tipoDocumento == usuarioDTO.TipoDocumento.GetHashCode()
                    && e.documento == usuarioDTO.Documento
                    && (!e.isExcluido));

                if (usuario != null)
                {
                    usuario.nome = usuarioDTO?.Nome ?? "";
                    usuario.telefone = usuarioDTO?.Telefone;
                    usuario.email = usuarioDTO?.Email ?? "";
                    usuario.dataAlteracao = DateTime.Now;

                    _pixAPIContext.Update(usuario);
                    _pixAPIContext.SaveChanges();
                }
                else
                {
                    usuario.tipoDocumento = Convert.ToByte(usuarioDTO?.TipoDocumento?.GetHashCode());
                    usuario.documento = usuarioDTO?.Documento ?? 0;
                    usuario.nome = usuarioDTO?.Nome ?? "";
                    usuario.telefone = usuarioDTO?.Telefone;
                    usuario.email = usuarioDTO?.Email ?? "";
                    usuario.dataInclusao = DateTime.Now;
                    usuario.isExcluido = false;

                    _pixAPIContext.Add(usuario);
                    _pixAPIContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void AtivarDesativarPeloDocumento(TipoDocumento tipoDocumento, long documento)
        {
            try
            {
                Usuario? usuario = _pixAPIContext.Usuario
                     .FirstOrDefault(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                         && e.documento == documento) 
                     ?? throw new BadRequestException("Usuário não encontrado.");

                if(usuario.isExcluido)
                {
                    usuario.dataAlteracao = DateTime.Now;
                    usuario.isExcluido = false;
                }
                else
                {
                    usuario.dataExclusao = DateTime.Now;
                    usuario.isExcluido = true;
                }

                _pixAPIContext.Update(usuario);
                _pixAPIContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

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

        public List<UsuarioDTO>? ListarTodos()
        {
            List<UsuarioDTO>? usuarios = new();
            try
            {
                usuarios = _pixAPIContext.Usuario
                    .Select(e => new UsuarioDTO(e)).ToList();

                if (!usuarios.Any())
                    throw new BadRequestException("Não foram encontrados usuários.");
            }
            catch (Exception e)
            {
                throw;
            }

            return usuarios;
        }

        public UsuarioDTO? BuscarAtivosPeloDocumento(TipoDocumento? tipoDocumento, long? documento) =>
            _pixAPIContext.Usuario.Where(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                && e.documento == documento
                && (!e.isExcluido))
            .Select(e => new UsuarioDTO(e)).FirstOrDefault() 
                ?? throw new BadRequestException("Usuário não encontrado.");

        public UsuarioDTO CadastrarOuAtualizar(TipoDocumento? tipoDocumento,
            long? documento, string? nome, long? telefone, string? email)
        {
            try
            {
                if ((tipoDocumento is null && documento is null) || documento is 0)
                    throw new BadRequestException("Necessário informar o documento.");

                UsuarioDTO? usuarioDTO = _pixAPIContext.Usuario
                    .Where(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                        && e.documento == documento)
                    .Select(e => new UsuarioDTO(e)).FirstOrDefault();

                Usuario usuario = new();

                if (usuarioDTO != null)
                {
                    usuario.nome = nome ?? usuarioDTO?.Nome;
                    usuario.telefone = telefone ?? usuarioDTO?.Telefone;
                    usuario.email = email ?? usuarioDTO?.Email;
                    usuario.dataInclusao = usuarioDTO.DataInclusao;
                    usuario.dataAlteracao = DateTime.Now;
                    usuario.isExcluido = false;

                    _pixAPIContext.Update(usuario);
                }
                else
                {
                    usuario.tipoDocumento = tipoDocumento.GetHashCode();
                    usuario.documento = documento ?? 0;
                    usuario.nome = nome ?? "";
                    usuario.telefone = telefone;
                    usuario.email = email ?? "";
                    usuario.dataInclusao = DateTime.Now;
                    usuario.isExcluido = false;

                    _pixAPIContext.Add(usuario);
                }

                _pixAPIContext.SaveChanges();

                return new(usuario);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void DesativarPeloDocumento(TipoDocumento tipoDocumento, long documento)
        {
            try
            {
                Usuario? usuario = _pixAPIContext.Usuario
                     .FirstOrDefault(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                         && e.documento == documento)
                     ?? throw new BadRequestException("Usuário não encontrado.");

                usuario.dataExclusao = DateTime.Now;
                usuario.isExcluido = true;

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

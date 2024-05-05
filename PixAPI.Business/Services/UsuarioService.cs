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

        public List<UsuarioDTO>? ListarTodosAtivos()
        {
            List<UsuarioDTO>? usuarios = new();
            try
            {
                usuarios = _pixAPIContext.Usuario
                    .Where(e => !e.isExcluido)
                    .Select(e => new UsuarioDTO(e)).ToList();

                if (!usuarios.Any())
                    throw new BadRequestException("Não foram encontrados usuários.");
            }
            catch (Exception)
            {
                throw;
            }

            return usuarios;
        }

        public UsuarioDTO? BuscarAtivoPeloDocumento(TipoDocumento? tipoDocumento, long? documento) =>
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
                    throw new BadRequestException("Documento obrigatório.");

                Usuario? usuario = _pixAPIContext.Usuario
                    .FirstOrDefault(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                        && e.documento == documento);

                if (usuario != null)
                {
                    usuario.nome = nome ?? usuario.nome;
                    usuario.telefone = telefone ?? usuario?.telefone;
                    usuario.email = email ?? usuario?.email;
                    usuario.dataAlteracao = DateTime.Now;
                    usuario.isExcluido = false;

                    _pixAPIContext.Update(usuario);
                }
                else
                {
                    usuario = new()
                    {
                        tipoDocumento = tipoDocumento.GetHashCode(),
                        documento = documento ?? 0,
                        nome = !string.IsNullOrWhiteSpace(nome)
                            ? nome : throw new BadRequestException("Nome obrigatório."),
                        telefone = telefone,
                        email = email ?? "",
                        dataInclusao = DateTime.Now,
                        isExcluido = false
                    };

                    _pixAPIContext.Add(usuario);
                }

                _pixAPIContext.SaveChanges();

                return new(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario DesativarPeloDocumento(TipoDocumento tipoDocumento, long documento)
        {
            try
            {
                Usuario? usuario = _pixAPIContext.Usuario
                     .FirstOrDefault(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                         && e.documento == documento
                         && (!e.isExcluido))
                     ?? throw new BadRequestException("Usuário não encontrado.");

                usuario.dataExclusao = DateTime.Now;
                usuario.isExcluido = true;

                _pixAPIContext.Update(usuario);
                _pixAPIContext.SaveChanges();

                return usuario;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

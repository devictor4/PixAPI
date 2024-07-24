using PixAPI.Business.DTOs.Usuarios;
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

        public List<UsuarioDTO>? ListarAtivos()
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

        public UsuarioDTO? BuscarAtivoPeloId(long id) =>
            _pixAPIContext.Usuario.Where(e => e.id == id && (!e.isExcluido))
            .Select(e => new UsuarioDTO(e)).FirstOrDefault() 
            ?? throw new BadRequestException("Usuário não encontrado.");

        public UsuarioDTO Cadastrar(TipoDocumento tipoDocumento, string documento,
            string email, string senha, string nome, short dddCelular, long celular)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(documento))
                    throw new BadRequestException("O documento é obrigatório.");

                Usuario? usuario = _pixAPIContext.Usuario
                    .FirstOrDefault(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                        && e.documento.Equals(documento));

                if (usuario != null) throw new BadRequestException("Usuário já cadastrado.");

                usuario = new()
                {
                    email = email,
                    senha = BCrypt.Net.BCrypt.HashPassword(senha),
                    nome = nome,
                    tipoDocumento = tipoDocumento.GetHashCode(),
                    documento = documento,
                    dddCelular = dddCelular,
                    celular = celular,
                    dataInclusao = DateTime.Now,
                    isExcluido = false
                };

                _pixAPIContext.Add(usuario);
                _pixAPIContext.SaveChanges();

                return new(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UsuarioDTO Alterar(TipoDocumento tipoDocumento, string documento,
            string? email, string? nome, short? dddCelular, long? celular)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(documento))
                    throw new BadRequestException("O documento é obrigatório.");

                Usuario? usuario = _pixAPIContext.Usuario
                    .FirstOrDefault(e => e.tipoDocumento == tipoDocumento.GetHashCode()
                        && e.documento.Equals(documento))
                    ?? throw new BadRequestException("Usuário não encontrado.");

                usuario.email = !string.IsNullOrWhiteSpace(email) ? email : usuario.email;
                usuario.nome = !string.IsNullOrWhiteSpace(nome) ? nome : usuario.nome;
                usuario.dddCelular = dddCelular.HasValue ? dddCelular.Value : usuario.dddCelular;
                usuario.celular = celular.HasValue ? celular.Value : usuario.celular;
                usuario.dataAlteracao = DateTime.Now;
                usuario.isExcluido = false;

                _pixAPIContext.Update(usuario);
                _pixAPIContext.SaveChanges();

                return new(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario DesativarPeloId(long id)
        {
            try
            {
                Usuario? usuario = _pixAPIContext.Usuario
                    .FirstOrDefault(e => e.id == id) 
                    ?? throw new BadRequestException("Usuário não encontrado.");

                if(usuario.isExcluido) throw new BadRequestException("O usuário está desativado.");

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

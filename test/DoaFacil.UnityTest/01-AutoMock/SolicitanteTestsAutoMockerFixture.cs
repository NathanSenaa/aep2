using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using DoaFacil.Pedidos.Domain.Models;
using Xunit;

namespace DoaFacil.UnityTest._01_AutoMock
{

    [CollectionDefinition(nameof(SolicitanteTestsAutoMockerCollection))]
    public class SolicitanteTestsAutoMockerCollection : ICollectionFixture<SolicitanteTestsAutoMockerFixture>
    {
    }
    public class SolicitanteTestsAutoMockerFixture : IDisposable
    {

        public Solicitante GerarClienteValido()
        {
            return GerarSolicitante(1).FirstOrDefault();
        }

        public IEnumerable<Solicitante> GerarSolicitante(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();           

            var solicitantes = new Faker<Solicitante>("pt_BR")
                .CustomInstantiator(f => new Solicitante(
                    f.Name.FirstName(genero),
                    f.Address.Locale.Trim(),
                    f.Phone.Locale.Trim(),
                    "",
                    f.Company.Cnpj()
                   ))
                .RuleFor(s => s.Email, (f, s) =>
                    f.Internet.Email(s.Nome.ToLower()));

            return solicitantes.Generate(quantidade);
        }

        public Solicitante GerarSolicitanteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var solicitante = new Faker<Solicitante>("pt_BR")
                .CustomInstantiator(f => new Solicitante(
                    f.Name.FirstName(genero),
                    f.Address.Locale.Trim(),
                    f.Phone.Locale.Trim(),
                    "",
                    "12345678912345"
                   ))
                .RuleFor(s => s.Email, (f, s) =>
                    f.Internet.Email(s.Nome.ToLower()));
            return solicitante;
        }

        public void Dispose()
        {
        }
    }
}

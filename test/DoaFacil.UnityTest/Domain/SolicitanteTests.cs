using DoaFacil.Core.DomainObjects;
using DoaFacil.UnityTest._01_AutoMock;
using Xunit;

namespace DoaFacil.UnityTest.Domain
{
    [Collection(nameof(SolicitanteTestsAutoMockerCollection))]
    public class SolicitanteTests
    {
        readonly SolicitanteTestsAutoMockerFixture _solicitanteTestAutoMockerFixture;

        public SolicitanteTests(SolicitanteTestsAutoMockerFixture solicitanteTestAutoMockerFixture)
        {
            _solicitanteTestAutoMockerFixture = solicitanteTestAutoMockerFixture;
        }

        [Fact(DisplayName = "Adicionar Solicitante com Sucesso")]
        [Trait("Categoria", "Cliente")]
        public void Solicitante_NovoSolicitante_DeveEstarValido()
        {
            //Arrange Act
            var solicitante = _solicitanteTestAutoMockerFixture.GerarSolicitante(1);

            Assert.NotNull(solicitante); // Verifica se o solicitante não é nulo
            Assert.False(string.IsNullOrEmpty(solicitante.First().Nome)); // Verifica se o nome do solicitante não é nulo ou vazio
            Assert.False(string.IsNullOrEmpty(solicitante.First().Endereco)); // Verifica se o endereço do solicitante não é nulo ou vazio
            Assert.False(string.IsNullOrEmpty(solicitante.First().Telefone)); // Verifica se o telefone do solicitante não é nulo ou vazio
            Assert.False(string.IsNullOrEmpty(solicitante.First().Email)); // Verifica se o email do solicitante não é nulo ou vazio
            Assert.False(string.IsNullOrEmpty(solicitante.First().Cnpj)); // Verifica se o CNPJ do solicitante não é nulo ou vazio 
        }

        [Fact(DisplayName = "Adicionar Solicitante com Sucesso")]
        [Trait("Categoria", "Cliente AutoMockFixture Tests")]
        public void Solicitante_NovoSolicitante_DeveEstarInValido()
        {
          //Arrange Act Assert
            Assert.Throws<DomainException>(() =>_solicitanteTestAutoMockerFixture.GerarSolicitanteInvalido());
        }
    }
}

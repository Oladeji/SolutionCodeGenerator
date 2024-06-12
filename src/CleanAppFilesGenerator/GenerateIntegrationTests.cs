
using System.Text;

namespace CleanAppFilesGenerator
{
    internal class GenerateIntegrationTests
    {
        public static string Generate(Type type, string thenamespace, string apiVersion)
        {

            var Output = new StringBuilder();
            string controllername = type.Name + "sControllerTest";
            Output.Append(ProduceTestHeader(thenamespace, type, apiVersion, controllername));
            Output.Append($"\n");
            Output.Append(ProduceControllerConstructor(controllername, type));
            Output.Append($"\n");
            Output.Append(RetunHttpStatusCode_OK_WhenDataExistInTheTable(thenamespace, type));
            Output.Append($"\n");
            Output.Append(Retun_HttpStatusCode_NotFound_WhenDoesNotExit(thenamespace, type));
            Output.Append($"\n");
            Output.Append(Return_Created_WhenObjectCreated(thenamespace, type));
            Output.Append($"\n");
            Output.Append(Post_Should_ReturnBadRequestWhen_DuplicateExist(thenamespace, type));
            Output.Append($"\n");
            Output.Append(Post_Should_ReturnObject_CorrectHeaderLocation_WhenSuccessful(thenamespace, type));
            Output.Append($"\n");
            Output.Append(Delete_Should_ReturnOk_WhenObjectIsDeletedSuccessfuly(thenamespace, type));
            Output.Append($"\n");
            Output.Append(Delete_Should_ReturnBadRequest_WhenObjectGuidDoesExists(thenamespace, type));
            Output.Append($"\n");
            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        private static string Delete_Should_ReturnBadRequest_WhenObjectGuidDoesExists(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)} [Theory]" +

           $"{GeneralClass.newlinepad(8)} [InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}\")]" +

           $"{GeneralClass.newlinepad(8)}public async Task Delete_{type.Name}Should_ReturnNotFoud_When{type.Name}GuidDoesExists(string path)" +
           $"{GeneralClass.newlinepad(8)}{{" +

           $"{GeneralClass.newlinepad(12)}//act" +
           $"{GeneralClass.newlinepad(12)}var result = await _httpClient.DeleteAsync($\"{{_baseUrl}}{{path}}/{{getRequestDTO.guidId}}\");\n" +



           $"{GeneralClass.newlinepad(12)}//assert" +
           $"{GeneralClass.newlinepad(12)}var problemDetail = await result.Content.ReadFromJsonAsync<ProblemDetails>();" +
           $"{GeneralClass.newlinepad(12)}result.StatusCode.Should().Be(HttpStatusCode.BadRequest);" +
            $"{GeneralClass.newlinepad(12)}problemDetail.Title.Should().Be(\"Data Not Found  in Repository\");" +
           $"{GeneralClass.newlinepad(12)}problemDetail.Type.Should().Be(\"A07\");" +
           $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string Delete_Should_ReturnOk_WhenObjectIsDeletedSuccessfuly(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)} [Theory]" +

             $"{GeneralClass.newlinepad(8)} [InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}\")]" +

             $"{GeneralClass.newlinepad(8)}public async Task Delete_{type.Name}Should_ReturnOk_WhenDeletedSuccessfuly(string path)" +
             $"{GeneralClass.newlinepad(8)}{{" +
              $"{GeneralClass.newlinepad(12)}var postresponse = await _httpClient.PostAsJsonAsync(path, getRequestDTO);\n" +


             $"{GeneralClass.newlinepad(12)}//act" +
             $"{GeneralClass.newlinepad(12)}var result = await _httpClient.DeleteAsync($\"{{_baseUrl}}{{path}}/{{getRequestDTO.guidId}}\");\n" +



             $"{GeneralClass.newlinepad(12)}//assert" +
             $"{GeneralClass.newlinepad(12)}result.StatusCode.Should().Be(HttpStatusCode.OK);" +

             $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string Post_Should_ReturnObject_CorrectHeaderLocation_WhenSuccessful(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)} [Theory]" +

              $"{GeneralClass.newlinepad(8)} [InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}\")]" +

              $"{GeneralClass.newlinepad(8)}public async Task Post_{type.Name}_Should_Create_CorrectHeaderLocation_WhenSuccessful(string path)" +
              $"{GeneralClass.newlinepad(8)}{{" +
              $"{GeneralClass.newlinepad(12)} var ExpetedHeaderLocation = $\"{{path}}/{{getRequestDTO.guidId}}\";\n" +
              $"{GeneralClass.newlinepad(12)}//act" +

              $"{GeneralClass.newlinepad(12)}var response = await _httpClient.PostAsJsonAsync(path, getRequestDTO);\n" +


              $"{GeneralClass.newlinepad(12)}//assert" +
              $"{GeneralClass.newlinepad(12)} response.Headers.Location?.OriginalString.Should().EndWith(ExpetedHeaderLocation);" +

              $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string Post_Should_ReturnBadRequestWhen_DuplicateExist(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)} [Theory]" +

             $"{GeneralClass.newlinepad(8)} [InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}\")]" +

             $"{GeneralClass.newlinepad(8)}public async Task Post_{type.Name}_Should_ReturnBadRequest_When_DuplicateExist(string path)" +
             $"{GeneralClass.newlinepad(8)}{{" +

             $"{GeneralClass.newlinepad(12)}//act" +
             $"{GeneralClass.newlinepad(12)}var ignoreResult = await _httpClient.PostAsJsonAsync(path, getRequestDTO);" +
             $"{GeneralClass.newlinepad(12)}var response = await _httpClient.PostAsJsonAsync(path, getRequestDTO);\n" +


             $"{GeneralClass.newlinepad(12)}//assert" +
             $"{GeneralClass.newlinepad(12)}response.StatusCode.Should().Be(HttpStatusCode.BadRequest);" +
             $"{GeneralClass.newlinepad(12)}var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();" +
              $"{GeneralClass.newlinepad(12)}problemDetail.Title.Should().Contain(\"Error Adding entity  into to Repository\");" +
             $"{GeneralClass.newlinepad(12)}problemDetail.Type.Should().Be(\"A04\");" +
             $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string Return_Created_WhenObjectCreated(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)} [Theory]" +

                  $"{GeneralClass.newlinepad(8)} [InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}\")]" +

                  $"{GeneralClass.newlinepad(8)}public async Task Post_{type.Name}_ShouldReturn_Created_WhenCreated(string path)" +
                  $"{GeneralClass.newlinepad(8)}{{" +
                   $"{GeneralClass.newlinepad(12)}var postresponse = await _httpClient.PostAsJsonAsync(path, getRequestDTO);" +

                  $"{GeneralClass.newlinepad(12)}//assert" +
                  $"{GeneralClass.newlinepad(12)}postresponse.StatusCode.Should().Be(HttpStatusCode.Created);" +


                  $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string Retun_HttpStatusCode_NotFound_WhenDoesNotExit(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)} [Theory]" +

             $"{GeneralClass.newlinepad(8)} [InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}/\", \"FIRSTSampleModelWRONG\")]" +
             $"{GeneralClass.newlinepad(8)} [InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}/\", \"99dcf5c5-5a00-4ffa-bb37-9374a8d3c69b\")]" +
             $"{GeneralClass.newlinepad(8)}public async Task Get_{type.Name}_ShouldRetun_HttpStatusCode_NotFound_WhenDoesNotExit(string path , string item)" +
             $"{GeneralClass.newlinepad(8)}{{\n" +

             $"{GeneralClass.newlinepad(12)}//act" +
             $"{GeneralClass.newlinepad(12)}var response = await _httpClient.GetAsync(path + item);\n" +

             $"{GeneralClass.newlinepad(12)}//assert" +
             $"{GeneralClass.newlinepad(12)}Assert.NotNull(response);" +
             $"{GeneralClass.newlinepad(12)}response.StatusCode.Should().Be(HttpStatusCode.NotFound);" +

             $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string RetunHttpStatusCode_OK_WhenDataExistInTheTable(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)} [Theory]" +
            $"{GeneralClass.newlinepad(8)}[InlineData($\"{{{thenamespace}APIEndPoints.{type.Name}.Controller}}\")]" +
            $"{GeneralClass.newlinepad(8)}public async Task Get_{type.Name}_ShouldRetunHttpStatusCode_OK_WhenDataExistInTheTable(string path)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}var postresponse = await _httpClient.PostAsJsonAsync(path, getRequestDTO);" +
            $"{GeneralClass.newlinepad(12)}var url = new Uri($\"{{_baseUrl}}{{path}}/{{getRequestDTO.guidId}}\");\n" +
            $"{GeneralClass.newlinepad(12)}//act" +
            $"{GeneralClass.newlinepad(12)}var response = await _httpClient.GetAsync(url);\n" +
            $"{GeneralClass.newlinepad(12)}//assert" +
            $"{GeneralClass.newlinepad(12)}var result = await response.Content.ReadFromJsonAsync<{type.Name}ResponseDTO>();" +
            $"{GeneralClass.newlinepad(12)}result.Should().BeAssignableTo<{type.Name}ResponseDTO>();" +
            $"{GeneralClass.newlinepad(12)}result.GuidId.Should().Be(getRequestDTO.guidId);" +
            $"{GeneralClass.newlinepad(12)}response.StatusCode.Should().Be(HttpStatusCode.OK);" +
            $"{GeneralClass.newlinepad(8)}}}";

        }

        private static string ProduceTestHeader(string name_space, Type type, string apiVersion, string controllername)
        {
            return ($"using AutoBogus;\n" +
            $"using FluentAssertions;\n" +
            $"using Microsoft.AspNetCore.Mvc;\n" +
            $"using Bogus;\n" +
            $"using System.Net.Http.Json;\n" +
            $"using System.Net;\n" +
            $"using {name_space}.Contracts.ResponseDTO.V1;\n" +
            $"using {name_space}.Api;\n" +
            $"using {name_space}.Contracts.RequestDTO.V1;\n" +
            $"using {name_space}.Integration.Tests.Base;\n" +
           $"namespace {name_space}.Integration.Tests.v1.{type.Name}Controller\n{{" +
            $"{GeneralClass.newlinepad(4)}public  class {controllername}  : BaseIntegrationTests" +
            $"{GeneralClass.newlinepad(4)}{{" +
            $"{GeneralClass.newlinepad(8)}private readonly Faker<{type.Name}CreateRequestDTO> faker;\n" +
            $"{GeneralClass.newlinepad(8)}private readonly {type.Name}CreateRequestDTO getRequestDTO;\n");

        }

        private static string ProduceControllerConstructor(string controllername, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}public {controllername}(IntegrationTestWebAppFactory factory) : base(factory)\n" +
                $"{GeneralClass.newlinepad(8)}{{" +
                $"{GeneralClass.newlinepad(8)}_httpClient.BaseAddress = new Uri(_baseUrl);\n" +
                $"{GeneralClass.newlinepad(8)}faker = new AutoFaker<{type.Name}CreateRequestDTO>()\n" +
                $"{GeneralClass.newlinepad(12)}//.RuleFor(x => x.name, f => f.Commerce.ProductName().PadRight(32, 'a').Remove(30))\n" +
                $"{GeneralClass.newlinepad(12)}//.RuleFor(x => x.description, f => f.Commerce.ProductDescription().PadRight(64, 'a').Remove(60))\n" +
                $"{GeneralClass.newlinepad(8)}.RuleFor(x => x.guidId, f => Guid.NewGuid());\n" +
                $"{GeneralClass.newlinepad(8)}getRequestDTO = faker.Generate();\n" +
                $"{GeneralClass.newlinepad(8)}}}" +
                $"\n" +
                $"{GeneralClass.newlinepad(8)}public Task InitializeAsync() => Task.CompletedTask;\n" +
                $"{GeneralClass.newlinepad(8)}public async Task DisposeAsync() {{}}";
        }

    }
}

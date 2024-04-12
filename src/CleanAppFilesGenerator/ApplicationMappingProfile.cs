
namespace CleanAppFilesGenerator
{
    internal class ApplicationMappingProfile
    {
        public static string Generate(Type type, string name_space, int selectedIndex, string apiVersion)
        {


            if (selectedIndex == 0)
            {
                return (
                $"using AutoMapper;\n" +
                $"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
                $"using {name_space}.Contracts.RequestDTO.V{apiVersion}.auto;\n" +
                $"using {name_space}.Domain.Entities;\n" +


                $"namespace {name_space}.Application.Mapping\n" +
                $"{{" +
                $"{GeneralClass.newlinepad(4)}public class MappingProfile : Profile" +
                $"{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}public MappingProfile()" +
                $"{GeneralClass.newlinepad(8)}{{" +
                $"{GenerateSpecific(type.Name)}");

            }
            else
            {

                return $"{GenerateSpecific(type.Name)}";
            }

        }
        public static string GenerateSpecific(string typeName)
        {

            return (
             $"{GeneralClass.newlinepad(12)}// {typeName} Mappings " +
            $"{GeneralClass.newlinepad(12)}//CreateMap<{typeName}GetRequestDTO, {typeName}>().ReverseMap();" +
            $"{GeneralClass.newlinepad(12)}//CreateMap<{typeName}GetRequestByIdDTO, {typeName}>().ReverseMap();" +
            $"{GeneralClass.newlinepad(12)}//CreateMap<{typeName}GetRequestByGuidDTO, {typeName}>().ReverseMap();" +
            $"{GeneralClass.newlinepad(12)}//CreateMap<{typeName}CreateRequestDTO, {typeName}>().ReverseMap();" +
            $"{GeneralClass.newlinepad(12)}CreateMap<{typeName}UpdateRequestDTO, {typeName}>().ReverseMap();" +
            $"{GeneralClass.newlinepad(12)}//CreateMap<{typeName}DeleteRequestDTO, {typeName}>().ReverseMap();" +
            $"{GeneralClass.newlinepad(8)}");




        }

    }
}
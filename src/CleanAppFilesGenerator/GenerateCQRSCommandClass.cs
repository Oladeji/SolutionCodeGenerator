﻿
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateCQRSCommandClass
    {
        //public static string GenerateCreateCommand(Type type, string name_space)
        //{
        //    var Output = new StringBuilder();
        //    Output.Append(ProduceCreateCommandHeader(name_space, type.Name));
        //    Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
        //    return Output.ToString();
        //}
        //public static string GenerateDeleteCommand(Type type, string name_space)
        //{
        //    var Output = new StringBuilder();
        //    Output.Append(ProduceDeleteCommandHeader(name_space, type.Name));
        //    Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
        //    return Output.ToString();
        //}

        //public static string GenerateUpdate(Type type, string name_space)
        //{
        //    var Output = new StringBuilder();
        //    Output.Append(ProduceUpdateCommandHeader(name_space, type.Name));
        //    Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
        //    return Output.ToString();
        //}

        public static string GenerateCQRSCommand(Type type, string name_space,string apiVersion, Func<string, string, string,string> produceheader)
        {
            var Output = new StringBuilder();
            Output.Append(produceheader(name_space, type.Name,apiVersion));
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }
        public static string ProduceCreateCommandHeader(string name_space, string entityName, string apiVersion)
        {
            return ($"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
                   $"using DomainErrors;\nusing LanguageExt;\nusing CQRSHelper;\n" +
            // $"namespace {name_space}.Application.CQRS.{entityName}.Commands\n{{{GeneralClass.newlinepad(4)}public  record Create{entityName}Command({entityName}CreateRequestDTO  Create{entityName}DTO) :  IRequest<Either<GeneralFailure, Guid>>;");
            $"namespace {name_space}.Application.CQRS\n{{{GeneralClass.newlinepad(4)}" +
            $"public  record Create{entityName}Command({entityName}CreateRequestDTO  Create{entityName}DTO) :  IRequest<Either<GeneralFailure, Guid>>;");

        }
        public static string ProduceDeleteCommandHeader(string name_space, string entityName, string apiVersion)
        {
            return ($"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
             $"using DomainErrors;\nusing LanguageExt;\nusing CQRSHelper;\n" +
         //$"namespace {name_space}.Application.CQRS.{entityName}.Commands\n" +
         $"namespace {name_space}.Application.CQRS\n" +
         $"{{{GeneralClass.newlinepad(4)}public  record Delete{entityName}Command({entityName}DeleteRequestDTO  Delete{entityName}DTO) :  IRequest<Either<GeneralFailure, int>>;");

        }

        public static string ProduceUpdateCommandHeader(string name_space, string entityName, string apiVersion)
        {
            return ($"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
                $"using DomainErrors;\nusing LanguageExt;\nusing CQRSHelper;\n" +
             //$"namespace {name_space}.Application.CQRS.{entityName}.Commands\n" +
             $"namespace {name_space}.Application.CQRS\n" +
             $"{{{GeneralClass.newlinepad(4)}public  record Update{entityName}Command({entityName}UpdateRequestDTO  Update{entityName}DTO) :  IRequest<Either<GeneralFailure, int>>;");

        }
        public static string ProduceCreateCommandHeader_NoMeadiatr(string name_space, string entityName, string apiVersion)
        {
            return ($"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
                   $"using {name_space}.Domain.Errors;\nusing LanguageExt;\n" +
            // $"namespace {name_space}.Application.CQRS.{entityName}.Commands\n{{{GeneralClass.newlinepad(4)}public  record Create{entityName}Command({entityName}CreateRequestDTO  Create{entityName}DTO) :  IRequest<Either<GeneralFailure, Guid>>;");
            $"namespace {name_space}.Application.CQRS\n{{{GeneralClass.newlinepad(4)}" +
            $"public  record Create{entityName}Command({entityName}CreateRequestDTO  Create{entityName}DTO) ;");

        }
        public static string ProduceDeleteCommandHeader_NoMeadiatr(string name_space, string entityName, string apiVersion)
        {
            return ($"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
         $"using {name_space}.Domain.Errors;\nusing LanguageExt;\n" +
         //$"namespace {name_space}.Application.CQRS.{entityName}.Commands\n" +
         $"namespace {name_space}.Application.CQRS\n" +
         $"{{{GeneralClass.newlinepad(4)}public  record Delete{entityName}Command({entityName}DeleteRequestDTO  Delete{entityName}DTO) ;");

        }


 

        public static string ProduceUpdateCommandHeader_NoMeadiatr(string name_space, string entityName, string apiVersion)
        {
            return ($"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
             $"using {name_space}.Domain.Errors;\nusing LanguageExt;\n" +
             //$"namespace {name_space}.Application.CQRS.{entityName}.Commands\n" +
             $"namespace {name_space}.Application.CQRS\n" +
             $"{{{GeneralClass.newlinepad(4)}public  record Update{entityName}Command({entityName}UpdateRequestDTO  Update{entityName}DTO) ;");

        }
    }
}
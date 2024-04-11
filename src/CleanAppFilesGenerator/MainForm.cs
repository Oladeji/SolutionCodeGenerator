using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //  int NoOfTimes = 0;
        int MaxNoOfItems = 0;
        int defaultStringlength = 32;
        public string identityDbContextName = "";
        public string generalInfo = "";
        public string checkBox1Info = "";
        public string apiVersion = "1";


        private void button2_Click(object sender, EventArgs e)
        {
            var generalInfoD = checkBox1.Checked;
            apiVersion = comboBox1.Text;
            try
            {
                EmptyFolders(FolderLocation.Text);
            }
            catch (Exception)
            {

                MessageBox.Show($"Problem Empting Directory:  {FolderLocation.Text} Please check if all files are closed");
            }


            //   NoOfTimes = 0;
            MaxNoOfItems = listBox1.Items.Count;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                //       NoOfTimes = i;
            }
            // Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Type[] tp;
            //label2.Text = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Domain_Model_Dll_Path.Text = openFileDialog1.FileName;

                try
                {

                    Library_Name_Space.Text = HelperClass.GetNodeNameFromXml(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "LibraryName");
                    FolderLocation.Text = HelperClass.GetNodeNameFromXml(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "OutPutDir");
                    identityDbContextName = HelperClass.GetNodeNameFromXml(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "IdentityDbContextName");
                    generalInfo = HelperClass.GetNodeNameFromXml(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "GeneralInfo");
                    checkBox1Info = HelperClass.GetNodeNameFromXml(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "UnCheckcheckBox1");
                    if (checkBox1Info.Trim() != "")
                    {
                        checkBox1.Checked = false;
                    }
                    if (generalInfo.Trim() != "")
                    {
                        label2.Text = generalInfo;
                        MessageBox.Show(generalInfo);

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Please include the library name in GenInfo.xml file");
                }

                try
                {

                    var assembly = Assembly.LoadFile(openFileDialog1.FileName);


                    Type[] exportedTypes = null;
                    try
                    {
                        exportedTypes = assembly.GetExportedTypes(); // .GetTypes();
                    }
                    catch (ReflectionTypeLoadException ee)
                    {
                        exportedTypes = ee.Types;
                    }





                    if (exportedTypes != null)
                    {
                        foreach (var type in exportedTypes)
                        {
                            if ((type.IsClass) && (!type.Name.Contains("BaseEntity")) && (!type.Name.Equals("BaseEvent")))
                            {

                                listBox1.Items.Add(type);
                            }


                        }
                    }


                }
                catch (Exception ee)
                {

                    MessageBox.Show("Problem with File" + ee.ToString());

                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox5.Clear();
            richTextBox6.Clear();
            // richTextBox19.Clear();

            string thenamespace = Library_Name_Space.Text;
            if (listBox1.SelectedItem != null)
            {
                Type type = (Type)listBox1.SelectedItem;

                CreateRequiredFolders(FolderLocation.Text, type);
                
                // richTextBox1.Text = GenerateEntityClass.GenerateBaseEntity(type, thenamespace);
                //richTextBox1.SaveFile(FolderLocation.Text + "\\Entities\\" + "BaseEntity.cs", RichTextBoxStreamType.PlainText);
                richTextBox1.Text = GenerateEntityClass.GenerateEntity(type, thenamespace);
                richTextBox1.SaveFile(FolderLocation.Text + "\\DomainAuto" + "\\" + type.Name + "\\" + type.Name + ".cs", RichTextBoxStreamType.PlainText);
                richTextBox1.SaveFile(FolderLocation.Text + "\\DomainAuto" + "\\" + type.Name + "\\" + type.Name + ".cs", RichTextBoxStreamType.PlainText);



                richTextBox2.Text = GenerateInterfaceClass.GenerateInterface(type, thenamespace);
                richTextBox2.SaveFile(FolderLocation.Text + "\\DomainAuto" + "\\" + type.Name + "\\" + "I" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);
                richTextBox2.SaveFile(FolderLocation.Text + "\\DomainAuto" + "\\" + type.Name + "\\" + "I" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);

                /// INFRASTRUCTURE LAYER
                /// GENERATE REPOSITORY IMPLEMENTATION AND ENTITYCONFIG FOR EACH ENTITY
                richTextBox3.Text = GenerateInfrastructureClass.GenerateRepositories(type, thenamespace);
                richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + type.Name + "\\" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);
                richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + type.Name + "\\" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);

                if (checkBox1.Checked)
                {
                    richTextBox3.Text = GenerateEntityConfigClass.GenerateEntityConfig(type, thenamespace, defaultStringlength);
                    richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + type.Name + "\\" + type.Name + "Config.cs", RichTextBoxStreamType.PlainText);
                    richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + type.Name + "\\" + type.Name + "Config.cs", RichTextBoxStreamType.PlainText);
                }
                //Commands folder
                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, apiVersion, GenerateCQRSCommandClass.ProduceCreateCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Commands\\" + "Create" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Commands\\" + "Create" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, apiVersion, GenerateCQRSCommandClass.ProduceDeleteCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Commands\\" + "Delete" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Commands\\" + "Delete" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, apiVersion, GenerateCQRSCommandClass.ProduceUpdateCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Commands\\" + "Update" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Commands\\" + "Update" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);

                //Handlers folder
                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, apiVersion, GenerateCQRSHandlerClass.ProduceCreateCommandHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Create" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Create" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, apiVersion, GenerateCQRSHandlerClass.ProduceDeleteCommandHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Delete" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Delete" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);


                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, apiVersion, GenerateCQRSHandlerClass.ProduceUpdateCommandhandler);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Update" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Update" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, apiVersion, GenerateCQRSHandlerClass.ProduceGetQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, apiVersion, GenerateCQRSHandlerClass.ProduceGetAllQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "GetAll" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "GetAll" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, apiVersion, GenerateCQRSHandlerClass.ProduceGetByIdQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByIdQueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByIdQueryHandler.cs", RichTextBoxStreamType.PlainText);


                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, apiVersion, GenerateCQRSHandlerClass.ProduceGetByGuidQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByGuidQueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByGuidQueryHandler.cs", RichTextBoxStreamType.PlainText);

                //Queries
                richTextBox6.Text = GenerateCQRSQueryClass.GenerateCQRSQuery(type, thenamespace, apiVersion);
                richTextBox6.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Queries\\" + "Get" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);
                richTextBox6.SaveFile(FolderLocation.Text + "\\CQRS\\" + type.Name + "\\Queries\\" + "Get" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);


                //richTextBox6.Text = GenerateCQRSQueryClass.GenerateCQRSQuey(type, thenamespace, GenerateCQRSQueryClass.ProduceGetAllQueryHeader);
                //richTextBox6.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Queries\\" + "GetAll" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);

                /*
                 * APPLICATION REQUEST AND RESPOSE DTO HAS BEEN REMOVED - I ONLY USE CONTRACTS NOW
                richTextBox4.Text = GenerateApplicationRequestDTOClass.GenerateRequest(type, thenamespace);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationRequestDTO\\" + "Application" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationRequestDTO\\" + "Application" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GenerateApplicationResponseDTOClass.GenerateResponse(type, thenamespace);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationResponseDTO\\" + "Application" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationResponseDTO\\" + "Application" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);

                */
                //Contracts
                richTextBox10.Text = GenerateContractRequestDTOClass.GenerateRequest(type, thenamespace, apiVersion);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractRequestDTO\\" + "Contract" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractRequestDTO\\" + "Contract" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);

                richTextBox10.Text = GenerateContractResponseDTOClass.GenerateResponse(type, thenamespace, apiVersion);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractResponseDTO\\" + "Contract" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractResponseDTO\\" + "Contract" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);



                richTextBox9.Text = GenerateControllers.Generate(type, thenamespace, apiVersion);
                richTextBox9.SaveFile(FolderLocation.Text + "\\Controllers\\" + type.Name + "sController.cs", RichTextBoxStreamType.PlainText);
                richTextBox9.SaveFile(FolderLocation.Text + "\\Controllers\\" + type.Name + "sController.cs", RichTextBoxStreamType.PlainText);


                //Use this login to generate a file that runs acros all the types in the dll


                if (listBox1.SelectedIndex == 0)
                {//IUNITOFWORK -INTERFACE

                    richTextBox7.Text = GenerateIUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex);
                    //UNITOFWORK
                    richTextBox12.Text = GenerateUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex);

                    //APIENDPOINTS
                    richTextBox8.Text = GenerateAPIEndPoints.Generate(type, thenamespace, listBox1.SelectedIndex);
                    //DBCONTEXT
                    richTextBox11.Text = GenerateDBContext.Generate(type, thenamespace, listBox1.SelectedIndex, identityDbContextName);

                    // this runs only once
                    richTextBox2.Text = GenerateInterfaceClass.GenerateIGenericRepository(thenamespace);
                    richTextBox2.SaveFile(FolderLocation.Text + "\\DomainAuto" + "\\" + "IGenericRepository.cs", RichTextBoxStreamType.PlainText);
                    richTextBox2.SaveFile(FolderLocation.Text + "\\DomainAuto" + "\\" + "IGenericRepository.cs", RichTextBoxStreamType.PlainText);

                    //Mapping Profile
                    richTextBox13.Text = ApplicationMappingProfile.Generate(type, thenamespace, listBox1.SelectedIndex, apiVersion);



                }
                else
                {
                    richTextBox7.AppendText(GenerateIUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex));
                    richTextBox12.AppendText(GenerateUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex));
                    richTextBox8.AppendText(GenerateAPIEndPoints.Generate(type, thenamespace, listBox1.SelectedIndex));
                    richTextBox11.AppendText(GenerateDBContext.Generate(type, thenamespace, listBox1.SelectedIndex, identityDbContextName));
                    //Mapping Profile
                    richTextBox13.AppendText(ApplicationMappingProfile.Generate(type, thenamespace, listBox1.SelectedIndex, apiVersion));


                }
                if (listBox1.SelectedIndex == MaxNoOfItems - 1)
                {
                    //IUNITOFWORK
                    richTextBox7.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox7.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox7.SaveFile(FolderLocation.Text + "\\DomainAuto\\" + "IUnitOfWork.cs", RichTextBoxStreamType.PlainText);
                    richTextBox7.SaveFile(FolderLocation.Text + "\\DomainAuto\\" + "IUnitOfWork.cs", RichTextBoxStreamType.PlainText);

                    //UNITOFWORK
                    richTextBox12.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox12.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox12.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + "UnitOfWork.cs", RichTextBoxStreamType.PlainText);
                    richTextBox12.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + "UnitOfWork.cs", RichTextBoxStreamType.PlainText);

                    //APIENDPOINTS
                    richTextBox8.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox8.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox8.SaveFile(FolderLocation.Text + "\\APIEndPoints\\" + thenamespace + "APIEndPoints.cs", RichTextBoxStreamType.PlainText);
                    richTextBox8.SaveFile(FolderLocation.Text + "\\APIEndPoints\\" + thenamespace + "APIEndPoints.cs", RichTextBoxStreamType.PlainText);

                    //DBCONTEXT
                    richTextBox11.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox11.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox11.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + thenamespace + "Context.cs", RichTextBoxStreamType.PlainText);
                    richTextBox11.SaveFile(FolderLocation.Text + "\\InfrastructureAuto\\" + thenamespace + "Context.cs", RichTextBoxStreamType.PlainText);

                    //Mapping Profile
                    richTextBox13.AppendText(ApplicationMappingProfile.Generate(type, thenamespace, listBox1.SelectedIndex, apiVersion));
                    richTextBox13.AppendText(GeneralClass.newlinepad(8) + "}");
                    richTextBox13.AppendText(GeneralClass.newlinepad(8) + "}");
                    richTextBox13.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox13.SaveFile(FolderLocation.Text + "\\Mapping\\" + type.Name + "MappingProfile.cs", RichTextBoxStreamType.PlainText);

                }
            }

        }

        private static void EmptyFolders(string basePath)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(basePath);

            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }
        private static void CreateRequiredFolders(string basePath, Type type)
        {
            // HelperClass.EnsureFolderIsCreated(basePath, "PartialEntities");
            HelperClass.EnsureFolderIsCreated(basePath, "DomainAuto" + "\\" + type.Name);
            HelperClass.EnsureFolderIsCreated(basePath, "InfrastructureAuto" + "\\" + type.Name);
            HelperClass.EnsureFolderIsCreated(basePath, "CQRS");
            //HelperClass.EnsureFolderIsCreated(basePath, "ApplicationRequestDTO");
            //HelperClass.EnsureFolderIsCreated(basePath, "ApplicationResponseDTO");
            HelperClass.EnsureFolderIsCreated(basePath, "CQRS");
            HelperClass.EnsureFolderIsCreated(basePath, "Mapping");
            HelperClass.EnsureFolderIsCreated(basePath, "Controllers");
            HelperClass.EnsureFolderIsCreated(basePath, "ContractRequestDTO");
            HelperClass.EnsureFolderIsCreated(basePath, "ContractResponseDTO");
            HelperClass.EnsureFolderIsCreated(basePath, "APIEndPoints");
            HelperClass.EnsureFolderIsCreated(basePath, "DBContext");
            //  HelperClass.EnsureFolderIsCreated(basePath + "\\ApplicationCQRS", type.Name);
            HelperClass.EnsureFolderIsCreated(basePath + "\\CQRS\\" + type.Name, "Commands");
            HelperClass.EnsureFolderIsCreated(basePath + "\\CQRS\\" + type.Name, "Handlers");
            HelperClass.EnsureFolderIsCreated(basePath + "\\CQRS\\" + type.Name, "Queries");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

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

        int NoOfTimes = 0;
        int MaxNoOfItems = 0;


        private void button2_Click(object sender, EventArgs e)
        {
            NoOfTimes = 0;
            MaxNoOfItems = listBox1.Items.Count;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                NoOfTimes = i;
            }
            Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Type[] tp;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Domain_Model_Dll_Path.Text = openFileDialog1.FileName;

                try
                {

                    Library_Name_Space.Text = HelperClass.GetNodeNameFromXml(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "LibraryName");
                    FolderLocation.Text = HelperClass.GetNodeNameFromXml(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "OutPutDir");
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

                            //DataTableAttribute[] dataTable = (DataTableAttribute[])item.GetCustomAttributes(typeof(DataTableAttribute), true);

                            //if (dataTable.Length > 0)
                            //{.
                            //    listBox1.Items.Add(item);

                            //}
                            //else
                            //{

                            //    NonDbAttribute[] data2 = (NonDbAttribute[])item.GetCustomAttributes(typeof(NonDbAttribute), true);

                            //    if (data2.Length > 0)
                            //    {
                            //        listBox1.Items.Add(item);

                            //    }
                            //}
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
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "PartialEntities");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "Interfaces\\Auto");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "InfrastructureRepository\\Auto\\" + type.Name);
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationCQRS");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationRequestDTO");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationResponseDTO");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationCQRS");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "Controllers");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ContractRequestDTO");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ContractResponseDTO");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "APIEndPoints");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "DBContext");



                //  HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS", type.Name);
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name, "Commands");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name, "Handlers");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name, "Queries");

                // richTextBox1.Text = GenerateEntityClass.GenerateBaseEntity(type, thenamespace);
                //richTextBox1.SaveFile(FolderLocation.Text + "\\Entities\\" + "BaseEntity.cs", RichTextBoxStreamType.PlainText);
                richTextBox1.Text = GenerateEntityClass.GenerateEntity(type, thenamespace);
                richTextBox1.SaveFile(FolderLocation.Text + "\\PartialEntities\\" + type.Name + ".cs", RichTextBoxStreamType.PlainText);
                richTextBox1.SaveFile(FolderLocation.Text + "\\PartialEntities\\" + type.Name + ".cs", RichTextBoxStreamType.PlainText);


                richTextBox2.Text = GenerateInterfaceClass.GenerateIGenericRepository(thenamespace);
                richTextBox2.SaveFile(FolderLocation.Text + "\\Interfaces\\Auto\\" + "IGenericRepository.cs", RichTextBoxStreamType.PlainText);
                richTextBox2.SaveFile(FolderLocation.Text + "\\Interfaces\\Auto\\" + "IGenericRepository.cs", RichTextBoxStreamType.PlainText);


                richTextBox2.Text = GenerateInterfaceClass.GenerateInterface(type, thenamespace);
                richTextBox2.SaveFile(FolderLocation.Text + "\\Interfaces\\Auto\\" + "I" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);
                richTextBox2.SaveFile(FolderLocation.Text + "\\Interfaces\\Auto\\" + "I" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);

                /// INFRASTRUCTURE LAYER
                /// GENERATE REPOSITORY IMPLEMENTATION AND ENTITYCONFIG FOR EACH ENTITY
                richTextBox3.Text = GenerateInfrastructureClass.GenerateRepositories(type, thenamespace);
                richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureRepository\\Auto\\" + type.Name + "\\" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);
                richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureRepository\\Auto\\" + type.Name + "\\" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);



                richTextBox3.Text = GenerateEntityConfigClass.GenerateEntityConfig(type, thenamespace);
                richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureRepository\\Auto\\" + type.Name + "\\" + type.Name + "EntityConfig.cs", RichTextBoxStreamType.PlainText);
                richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureRepository\\Auto\\" + type.Name + "\\" + type.Name + "EntityConfig.cs", RichTextBoxStreamType.PlainText);

                //Commands folder
                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, GenerateCQRSCommandClass.ProduceCreateCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Create" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Create" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, GenerateCQRSCommandClass.ProduceDeleteCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Delete" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Delete" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, GenerateCQRSCommandClass.ProduceUpdateCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Update" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Update" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);

                //Handlers folder
                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceCreateCommandHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Create" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Create" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceDeleteCommandHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Delete" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Delete" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);


                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceUpdateCommandhandler);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Update" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Update" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceGetQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceGetAllQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "GetAll" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "GetAll" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);

                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceGetByIdQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByIdQueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByIdQueryHandler.cs", RichTextBoxStreamType.PlainText);


                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceGetByGuidQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByGuidQueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "ByGuidQueryHandler.cs", RichTextBoxStreamType.PlainText);

                //Queries
                richTextBox6.Text = GenerateCQRSQueryClass.GenerateCQRSQuery(type, thenamespace);
                richTextBox6.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Queries\\" + "Get" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);
                richTextBox6.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Queries\\" + "Get" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);
                //richTextBox6.Text = GenerateCQRSQueryClass.GenerateCQRSQuey(type, thenamespace, GenerateCQRSQueryClass.ProduceGetAllQueryHeader);
                //richTextBox6.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Queries\\" + "GetAll" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GenerateApplicationRequestDTOClass.GenerateRequest(type, thenamespace);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationRequestDTO\\" + "Application" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationRequestDTO\\" + "Application" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GenerateApplicationResponseDTOClass.GenerateResponse(type, thenamespace);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationResponseDTO\\" + "Application" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationResponseDTO\\" + "Application" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);

                //Contracts
                richTextBox10.Text = GenerateContractRequestDTOClass.GenerateRequest(type, thenamespace);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractRequestDTO\\" + "Contract" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractRequestDTO\\" + "Contract" + type.Name + "RequestDTO.cs", RichTextBoxStreamType.PlainText);

                richTextBox10.Text = GenerateContractResponseDTOClass.GenerateResponse(type, thenamespace);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractResponseDTO\\" + "Contract" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);
                richTextBox10.SaveFile(FolderLocation.Text + "\\ContractResponseDTO\\" + "Contract" + type.Name + "ResponseDTO.cs", RichTextBoxStreamType.PlainText);



                richTextBox9.Text = GenerateControllers.Generate(type, thenamespace, comboBox1.Text);
                richTextBox9.SaveFile(FolderLocation.Text + "\\Controllers\\" + type.Name + "sController.cs", RichTextBoxStreamType.PlainText);
                richTextBox9.SaveFile(FolderLocation.Text + "\\Controllers\\" + type.Name + "sController.cs", RichTextBoxStreamType.PlainText);

                //Use this login to generate a file that runs acros all the types in the dll
                if (listBox1.SelectedIndex == 0)
                {//IUNITOFWORK//APIENDPOINTS//DBCONTEXT
                    richTextBox7.Text = GenerateIUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex);
                    richTextBox8.Text = GenerateAPIEndPoints.Generate(type, thenamespace, listBox1.SelectedIndex);
                    richTextBox11.Text = GenerateDBContext.Generate(type, thenamespace, listBox1.SelectedIndex);

                }
                else
                {
                    richTextBox7.AppendText(GenerateIUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex));
                    richTextBox8.AppendText(GenerateAPIEndPoints.Generate(type, thenamespace, listBox1.SelectedIndex));
                    richTextBox11.AppendText(GenerateDBContext.Generate(type, thenamespace, listBox1.SelectedIndex));
                }
                if (listBox1.SelectedIndex == MaxNoOfItems - 1)
                {

                    richTextBox7.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox7.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox7.SaveFile(FolderLocation.Text + "\\Interfaces\\" + "IUnitOfWork.cs", RichTextBoxStreamType.PlainText);
                    richTextBox7.SaveFile(FolderLocation.Text + "\\Interfaces\\" + "IUnitOfWork.cs", RichTextBoxStreamType.PlainText);


                    richTextBox8.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox8.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox8.SaveFile(FolderLocation.Text + "\\APIEndPoints\\" + thenamespace + "APIEndPoints.cs", RichTextBoxStreamType.PlainText);
                    richTextBox8.SaveFile(FolderLocation.Text + "\\APIEndPoints\\" + thenamespace + "APIEndPoints.cs", RichTextBoxStreamType.PlainText);

                    richTextBox11.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox11.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox11.SaveFile(FolderLocation.Text + "\\DBContext\\" + thenamespace + "Context.cs", RichTextBoxStreamType.PlainText);
                    richTextBox11.SaveFile(FolderLocation.Text + "\\DBContext\\" + thenamespace + "Context.cs", RichTextBoxStreamType.PlainText);


                }
            }

        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

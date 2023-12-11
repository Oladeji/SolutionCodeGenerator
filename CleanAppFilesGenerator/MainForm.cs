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
        //int nooftimesb = 0;
        //int nooftimesc = 0;
        //int nooftimesbackp = 0;
        //private bool firsttime = true;

        private void button2_Click(object sender, EventArgs e)
        {
            NoOfTimes = 0;
            MaxNoOfItems = listBox1.Items.Count;
            //nooftimesb = 0;
            //nooftimesc = 0;
            //nooftimesbackp = 0;
            //firsttime = true;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                NoOfTimes = i;
            }
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
                            if ((type.IsClass) && (!type.Name.Contains("BaseEntity")))
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
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "PartialEntities");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "Interfaces");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "InfrastructureRepository");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationCQRS");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationRequestDTO");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationResponseDTO");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "ApplicationCQRS");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text, "Controllers");
                //HelperClass.EnsureFolderIsCreated(FolderLocation.Text+ "\\ApplicationCQRS", "ApplicationCQRS");


                //if (!Directory.Exists(FolderLocation.Text + "\\Models"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Models");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Modelsless"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Modelsless");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Dev"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Dev");
                //}

                //if (!Directory.Exists(FolderLocation.Text + "\\AutoController"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\AutoController");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Repository"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Repository");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\IService"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\IService");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Service"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Service");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Others"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Others");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Core"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Core");
                //}

                //if (!Directory.Exists(FolderLocation.Text + "\\AutoController"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\AutoController");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Repository"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Repository");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\IService"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\IService");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Service"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Service");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Others"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Others");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\Core"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\Core");
                //}
                //if (!Directory.Exists(FolderLocation.Text + "\\BackUps"))
                //{
                //    Directory.CreateDirectory(FolderLocation.Text + "\\BackUps");
                //}



                // string classname, Classlistname, result, resultless;

                Type type = (Type)listBox1.SelectedItem;

                //  HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS", type.Name);
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name, "Commands");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name, "Handlers");
                HelperClass.EnsureFolderIsCreated(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name, "Queries");

                // richTextBox1.Text = GenerateEntityClass.GenerateBaseEntity(type, thenamespace);
                //richTextBox1.SaveFile(FolderLocation.Text + "\\Entities\\" + "BaseEntity.cs", RichTextBoxStreamType.PlainText);
                richTextBox1.Text = GenerateEntityClass.GenerateEntity(type, thenamespace);
                richTextBox1.SaveFile(FolderLocation.Text + "\\PartialEntities\\" + type.Name + ".cs", RichTextBoxStreamType.PlainText);


                richTextBox2.Text = GenerateInterfaceClass.GenerateIGenericRepository(thenamespace);
                richTextBox2.SaveFile(FolderLocation.Text + "\\Interfaces\\" + "IGenericRepository.cs", RichTextBoxStreamType.PlainText);


                richTextBox2.Text = GenerateInterfaceClass.GenerateInterface(type, thenamespace);
                richTextBox2.SaveFile(FolderLocation.Text + "\\Interfaces\\" + "I" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);


                richTextBox3.Text = GenerateInfrastructureClass.GenerateRepositories(type, thenamespace);
                richTextBox3.SaveFile(FolderLocation.Text + "\\InfrastructureRepository\\" + type.Name + "Repository.cs", RichTextBoxStreamType.PlainText);



                //Commands folder
                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, GenerateCQRSCommandClass.ProduceCreateCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Create" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, GenerateCQRSCommandClass.ProduceDeleteCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Delete" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);
                richTextBox4.Text = GenerateCQRSCommandClass.GenerateCQRSCommand(type, thenamespace, GenerateCQRSCommandClass.ProduceUpdateCommandHeader);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Commands\\" + "Update" + type.Name + "Command.cs", RichTextBoxStreamType.PlainText);


                //Handlers folder
                //richTextBox4.Text = GenerateCQRSHandlerClass.GenerateCreateCommandhandler(type, thenamespace);
                //richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Create" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                //richTextBox4.Text = GenerateCQRSHandlerClass.GenerateDeleteCommandHandler(type, thenamespace);
                //richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Delete" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                //richTextBox4.Text = GenerateCQRSHandlerClass.GenerateUpdateCommandhandler(type, thenamespace);
                //richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Update" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                //richTextBox4.Text = GenerateCQRSHandlerClass.GenerateGetQueryHandler(type, thenamespace);
                //richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);
                //richTextBox4.Text = GenerateCQRSHandlerClass.GenerateGetAllQueryHandler(type, thenamespace);
                //richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "GetAll" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceCreateCommandHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Create" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceDeleteCommandHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Delete" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceUpdateCommandhandler);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Update" + type.Name + "CommandHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceGetQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "Get" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);
                richTextBox5.Text = GenerateCQRSHandlerClass.GenerateCQRSHandler(type, thenamespace, GenerateCQRSHandlerClass.ProduceGetAllQueryHandlerHeader);
                richTextBox5.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Handlers\\" + "GetAll" + type.Name + "QueryHandler.cs", RichTextBoxStreamType.PlainText);

                //Queries
                richTextBox6.Text = GenerateCQRSQueryClass.GenerateCQRSQuey(type, thenamespace, GenerateCQRSQueryClass.ProduceGetQueryHeader);
                richTextBox6.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Queries\\" + "Get" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);
                richTextBox6.Text = GenerateCQRSQueryClass.GenerateCQRSQuey(type, thenamespace, GenerateCQRSQueryClass.ProduceGetAllQueryHeader);
                richTextBox6.SaveFile(FolderLocation.Text + "\\ApplicationCQRS\\" + type.Name + "\\Queries\\" + "GetAll" + type.Name + "Query.cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GeneratRequestDTOClass.GenerateRequest(type, thenamespace);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationRequestDTO\\" + "ApplicationRequest" + type.Name + ".cs", RichTextBoxStreamType.PlainText);

                richTextBox4.Text = GeneratResponseDTOClass.GenerateResponse(type, thenamespace);
                richTextBox4.SaveFile(FolderLocation.Text + "\\ApplicationResponseDTO\\" + "ApplicationResponse" + type.Name + ".cs", RichTextBoxStreamType.PlainText);


                richTextBox8.Text = GenerateControllers.Generate(type, thenamespace, comboBox1.Text);
                richTextBox8.SaveFile(FolderLocation.Text + "\\Controllers\\" + type.Name + "sController.cs", RichTextBoxStreamType.PlainText);



                if (listBox1.SelectedIndex == 0)
                {//IUNITOFWORK
                    richTextBox7.Text = GenerateIUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex);

                }
                else
                {
                    richTextBox7.AppendText(GenerateIUnitOfWork.Generate(type, thenamespace, listBox1.SelectedIndex));
                }
                if (listBox1.SelectedIndex == MaxNoOfItems - 1)
                {

                    richTextBox7.AppendText(GeneralClass.newlinepad(4) + "}");
                    richTextBox7.AppendText(GeneralClass.newlinepad(0) + "}");
                    richTextBox7.SaveFile(FolderLocation.Text + "\\Interfaces\\" + "IUnitOfWork.cs", RichTextBoxStreamType.PlainText);
                }
            }

        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

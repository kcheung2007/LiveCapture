using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LiveCapture
{
    public partial class UcExplorer : UserControl
    {
        //public event UpdateStatusEventHandler statusChanged;
        public event EventHandler<StatusEventArgs> statusChanged;

        public UcExplorer()
        {
            InitializeComponent();
        }

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        private void btnGo_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcExplorer.cs - btnGo_Click: " + cboUri.Text);
            webBrowser.Navigate(cboUri.Text);
            // webBrowser.Navigate("http://www.yahoo.com");

            SaveComboBoxItem();
        }//end of btnGo_Click

        private void btnOpenFile_Click(object sender, EventArgs e)
        {

        }//end of btnGo_Click

        private void cboUri_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cboUri_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcExplorer.cs - SaveComboBoxItem");
            // Save the combox
            XmlTextWriter tw = new XmlTextWriter(Application.StartupPath + "\\URL.xml", System.Text.Encoding.UTF8);

            Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\URL.xml");

            tw.WriteStartDocument();
            tw.WriteStartElement("comboboxes");
            WriteComboBoxEntries(cboUri, "cboUri", cboUri.Text, tw);

            // save for reference - add more combo box
            // WriteComboBoxEntries(cboTest2, "cboTest2", txtTest2.Text, tw);
            // WriteComboBoxEntries(cboTest3, "cboTest3", txtTest3.Text, tw);

            tw.WriteEndElement();
            tw.Flush();
            tw.Close();
            LoadComboBoxes();
        }//end of SaveComboBoxItem

        /// <summary>
        /// Write a list of combox box entries into an xml file
        /// </summary>
        /// <param name="cboBox">ComboBox control</param>
        /// <param name="cboBoxName">Name of the control in XML</param>
        /// <param name="cboBoxText">The input text in combo box</param>
        /// <param name="tw">XmlTextWriter</param>
        private void WriteComboBoxEntries(ComboBox cboBox, string cboBoxName, string txtBoxText, XmlTextWriter tw)
        {
            Debug.WriteLine("UcExplorer.cs - WriteComboBoxEntries");
            int maxEntriesToStore = 10;

            tw.WriteStartElement("combobox");
            tw.WriteStartAttribute("name", string.Empty);
            tw.WriteString(cboBoxName);
            tw.WriteEndAttribute();

            // Write the item from the text box first.
            if (txtBoxText.Length != 0)
            {
                tw.WriteStartElement("entry");
                tw.WriteString(txtBoxText);
                tw.WriteEndElement();
                maxEntriesToStore -= 1;
            }//end of if

            // Write the rest of the entries (up to 10).
            for (int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
            {
                if (txtBoxText != cboBox.Items[i].ToString())
                {
                    tw.WriteStartElement("entry");
                    tw.WriteString(cboBox.Items[i].ToString());
                    tw.WriteEndElement();
                }
            }//end of for
            tw.WriteEndElement();
        }//end of WriteComboBoxEntries

        private void LoadComboBoxes()
        {
            Debug.WriteLine("UcExplorer.cs - LoadComboBoxes");
            try
            {
                cboUri.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\URL.xml";
                if (!File.Exists(cboPath))
                {
                    //                File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load(cboPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                // Add items from the xml file to the combobox.
                for (int j = 0; j < nodeList.Item(0).ChildNodes.Count; ++j)
                {
                    cboUri.Items.Add(nodeList.Item(0).ChildNodes.Item(j).InnerText);
                }//end of for
            }//end of try
            catch (SystemException ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
            }//end of catch
        }//end of LoadComboBoxes

    }
}

#define DEBUG
using EuroMillionsHelper.HelperMethods;
using EuroMillionsHelper.Model;
using EuroMillionsHelper.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EuroMillionsHelper
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
      // Add any other initialisation variables
    }

    public readonly Dictionary<string, string> _languageDicoEn = new Dictionary<string, string>();
    public readonly Dictionary<string, string> _languageDicoFr = new Dictionary<string, string>();
    private readonly Tirages listTirages = new Tirages();
    private string _currentLanguage = "english";
    private ConfigurationOptions _configurationOptions = new ConfigurationOptions();
    public Dictionary<int, int> nombreDeSortieBoules = new Dictionary<int, int>();
    public Dictionary<int, int> nombreDeSortieEtoiles = new Dictionary<int, int>();
    public Dictionary<int, DateTime> bouleSortieDepuisXJour = new Dictionary<int, DateTime>();
    public Dictionary<int, DateTime> etoileSortieDepuisXJour = new Dictionary<int, DateTime>();

    private void QuitToolStripMenuItemClick(object sender, EventArgs e)
    {
      SaveWindowValue();
      Application.Exit();
    }

    private void AboutToolStripMenuItemClick(object sender, EventArgs e)
    {
      AboutBoxApplication aboutBoxApplication = new AboutBoxApplication();
      aboutBoxApplication.ShowDialog();
    }

    public static string DisplayTitle()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      return $"V{fvi.FileMajorPart}.{fvi.FileMinorPart}.{fvi.FileBuildPart}.{fvi.FilePrivatePart}";
    }

    private void FormMainLoad(object sender, EventArgs e)
    {
      LoadSettingsAtStartup();
      InitializeDictionaries();
      LoadDictionaries();
      LoadNumberOfBallsDrawn();
      ClearAllBalls();
    }

    private void LoadDictionaries()
    {
      foreach (Tirage tirage in listTirages.ListOfTirages)
      {
        nombreDeSortieBoules[tirage.Boule1]++;
        nombreDeSortieBoules[tirage.Boule2]++;
        nombreDeSortieBoules[tirage.Boule3]++;
        nombreDeSortieBoules[tirage.Boule4]++;
        nombreDeSortieBoules[tirage.Boule5]++;

        nombreDeSortieEtoiles[tirage.Etoile1]++;
        nombreDeSortieEtoiles[tirage.Etoile2]++;

        if (bouleSortieDepuisXJour[tirage.Boule1] < tirage.DateTirage)
        {
          bouleSortieDepuisXJour[tirage.Boule1] = tirage.DateTirage;
        }

        if (bouleSortieDepuisXJour[tirage.Boule2] < tirage.DateTirage)
        {
          bouleSortieDepuisXJour[tirage.Boule2] = tirage.DateTirage;
        }

        if (bouleSortieDepuisXJour[tirage.Boule3] < tirage.DateTirage)
        {
          bouleSortieDepuisXJour[tirage.Boule3] = tirage.DateTirage;
        }

        if (bouleSortieDepuisXJour[tirage.Boule4] < tirage.DateTirage)
        {
          bouleSortieDepuisXJour[tirage.Boule4] = tirage.DateTirage;
        }

        if (bouleSortieDepuisXJour[tirage.Boule5] < tirage.DateTirage)
        {
          bouleSortieDepuisXJour[tirage.Boule5] = tirage.DateTirage;
        }
      }
    }

    private void AssignDate(int number, DateTime theDate)
    {
      bouleSortieDepuisXJour[number] = theDate;
    }

    public static Dictionary<int, int> SortDicoAscending(Dictionary<int, int> dico)
    {
      return dico.OrderBy(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public static Dictionary<int, int> SortDicoDescending(Dictionary<int, int> dico)
    {
      return dico.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public static Dictionary<int, DateTime> SortDicoDescending(Dictionary<int, DateTime> dico)
    {
      return dico.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public static Dictionary<int, DateTime> SortDicoAscending(Dictionary<int, DateTime> dico)
    {
      return dico.OrderBy(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
    }


    private void InitializeDictionaries()
    {
      nombreDeSortieBoules = new Dictionary<int, int>();
      for (int i = 1; i < 51; i++)
      {
        nombreDeSortieBoules.Add(i, 0);
      }

      nombreDeSortieEtoiles = new Dictionary<int, int>();
      for (int i = 1; i < 13; i++)
      {
        nombreDeSortieEtoiles.Add(i, 0);
      }

      bouleSortieDepuisXJour = new Dictionary<int, DateTime>();
      for (int i = 1; i < 51; i++)
      {
        bouleSortieDepuisXJour.Add(i, new DateTime(1, 1, 1));
      }
    }

    private void LoadSettingsAtStartup()
    {
      Text += $" {DisplayTitle()}";
      GetWindowValue();
      LoadLanguages();
      SetLanguage(Settings.Default.LastLanguageUsed);
      InitializeHistoryListView(listViewHistory);
      LoadHistoryDraws();
      InitializeDataGridView();
    }

    private void LoadNumberOfBallsDrawn()
    {
      dataGridViewNumberOfBallsDrawn.Rows.Clear();
      int[] resultBoule = new int[51];
      for (int i = 0; i < resultBoule.Length; i++)
      {
        resultBoule[i] = 0;
      }

      int[] resultEtoile = new int[13];
      for (int i = 0; i < resultEtoile.Length; i++)
      {
        resultEtoile[i] = 0;
      }

      foreach (Tirage tirage in listTirages.ListOfTirages)
      {
        resultBoule[tirage.Boule1]++;
        resultBoule[tirage.Boule2]++;
        resultBoule[tirage.Boule3]++;
        resultBoule[tirage.Boule4]++;
        resultBoule[tirage.Boule5]++;
        resultEtoile[tirage.Etoile1]++;
        resultEtoile[tirage.Etoile2]++;
      }

      dataGridViewNumberOfBallsDrawn.Rows.Add(resultBoule[1],
        resultBoule[2],
        resultBoule[3],
        resultBoule[4],
        resultBoule[5],
        resultBoule[6],
        resultBoule[7],
        resultBoule[8],
        resultBoule[9],
        resultBoule[10],
        resultBoule[11],
        resultBoule[12],
        resultBoule[13],
        resultBoule[14],
        resultBoule[15],
        resultBoule[16],
        resultBoule[17],
        resultBoule[18],
        resultBoule[19],
        resultBoule[20],
        resultBoule[21],
        resultBoule[22],
        resultBoule[23],
        resultBoule[24],
        resultBoule[25],
        resultBoule[26],
        resultBoule[27],
        resultBoule[28],
        resultBoule[29],
        resultBoule[30],
        resultBoule[31],
        resultBoule[32],
        resultBoule[33],
        resultBoule[34],
        resultBoule[35],
        resultBoule[36],
        resultBoule[37],
        resultBoule[38],
        resultBoule[39],
        resultBoule[40],
        resultBoule[41],
        resultBoule[42],
        resultBoule[43],
        resultBoule[44],
        resultBoule[45],
        resultBoule[46],
        resultBoule[47],
        resultBoule[48],
        resultBoule[49],
        resultBoule[50]
        );

      foreach (var pair in SortDicoDescending(nombreDeSortieBoules))
      {
        string zeroPadded = pair.Key < 10 ? "0" : "";
        listBoxLesPlusSortie.Items.Add($"{zeroPadded}{pair.Key} - {pair.Value}");
      }

      foreach (var pair in bouleSortieDepuisXJour)
      {
        string zeroPadded = pair.Key < 10 ? "0" : "";
        var days = (DateTime.Now - pair.Value).TotalDays;
        int numberOfDays = (int)Math.Floor(days);
        listBoxPasSortieDepuis.Items.Add($"{zeroPadded}{pair.Key} - {numberOfDays}");
      }

      foreach (var pair in SortDicoDescending(bouleSortieDepuisXJour))
      {
        string zeroPadded = pair.Key < 10 ? "0" : "";
        var days = (DateTime.Now - pair.Value).TotalDays;
        int numberOfDays = (int)Math.Floor(days);
        listBoxLesPlusRecents.Items.Add($"{zeroPadded}{pair.Key} - {numberOfDays}");
      }

      foreach (var pair in SortDicoAscending(bouleSortieDepuisXJour))
      {
        string zeroPadded = pair.Key < 10 ? "0" : "";
        var days = (DateTime.Now - pair.Value).TotalDays;
        int numberOfDays = (int)Math.Floor(days);
        listBoxLesPlusAnciens.Items.Add($"{zeroPadded}{pair.Key} - {numberOfDays}");
      }


    }

    private void InitializeDataGridView()
    {
      dataGridViewNumberOfBallsDrawn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      for (int i = 1; i <= 50; i++)
      {
        dataGridViewNumberOfBallsDrawn.Columns.Add($"number{i}", $"{i}");
      }
    }

    private static void InitializeHistoryListView(ListView listview)
    {
      listview.BackColor = SystemColors.Control;
      listview.Dock = DockStyle.Fill;
      listview.TabIndex = 0;
      listview.View = View.Details;
      listview.MultiSelect = true;
      listview.HideSelection = false;
      listview.HeaderStyle = ColumnHeaderStyle.Clickable;
      listview.Columns.Add("boule1", "Boule 1");
      listview.Columns.Add("boule2", "Boule 2");
      listview.Columns.Add("boule3", "Boule 3");
      listview.Columns.Add("boule4", "Boule 4");
      listview.Columns.Add("boule5", "Boule 5");
      listview.Columns.Add("etoile1", "Etoile 1");
      listview.Columns.Add("etoile2", "Etoile 2");
      ResizeListViewColumns(listview);
    }

    private static void ResizeListViewColumns(ListView lv)
    {
      if (lv.Columns.Count == 0)
      {
        return;
      }

      for (int i = 0; i < lv.Columns.Count - 1; i++)
      {
        lv.AutoResizeColumn(i, GetLongestString(lv.Columns[i].Text, lv, i));
      }
    }

    private void LoadHistoryDraws()
    {
      listViewHistory.Items.Clear();

      // reading archive files
      // foreach file in archives folder
      // read file and add lines to listviewHistory
      var test = Directory.GetFiles(@".\Archives\", "euromillions*.csv");
      foreach (var fileName in Directory.GetFiles(@".\Archives\", "euromillions*.csv"))
      {
        var file = ReadCsvFile(fileName);
        foreach (Tirage unTirage in file)
        {
          ListViewItem listViewItem = new ListViewItem(unTirage.Boule1.ToString());
          listViewItem.SubItems.Add(unTirage.Boule2.ToString());
          listViewItem.SubItems.Add(unTirage.Boule3.ToString());
          listViewItem.SubItems.Add(unTirage.Boule4.ToString());
          listViewItem.SubItems.Add(unTirage.Boule5.ToString());
          listViewItem.SubItems.Add(unTirage.Etoile1.ToString());
          listViewItem.SubItems.Add(unTirage.Etoile2.ToString());

          listTirages.Add(unTirage);
          listViewHistory.Items.Add(listViewItem);
        }
      }

      ResizeListViewColumns(listViewHistory);
    }

    private List<Tirage> ReadCsvFile(string fileName)
    {
      List<Tirage> result = new List<Tirage>();
      string firstLine = string.Empty;
      int numberOfLines = 0;
      List<string> tmpLines = new List<string>();
      try
      {
        using (StreamReader sr = new StreamReader(fileName))
        {
          while (!sr.EndOfStream)
          {
            tmpLines.Add(sr.ReadLine());
          }
        }
      }
      catch (Exception) { }

      firstLine = tmpLines[0];
      numberOfLines = tmpLines.Count - 1;
      int index = 4;
      string stringToSearch = "boule_1";
      int indexDate = 2;
      if (fileName.Contains("2020"))
      {
        stringToSearch = "N1";
        indexDate = 1;
      }

      string[] indexArray = firstLine.Split(';');
      index = GetPositionOfString(indexArray, stringToSearch);

      for (int i = 1; i < numberOfLines; i++)
      {
        var tmp = tmpLines[i];
        // if (i == 0) continue;
        var tmpNumbers = tmp.Split(';');
        //20201024   euromillions.csv
        //01234567
        //31/01/2014 euromillions2.csv
        //0123456789
        // default values
        int annee = 1;
        int mois = 1;
        int jour = 1;
        if (fileName.Contains("dateUS") && fileName.Contains("NoSlash"))
        {
          annee = int.Parse(tmpNumbers[indexDate].Substring(0, 4));
          mois = int.Parse(tmpNumbers[indexDate].Substring(4, 2));
          jour = int.Parse(tmpNumbers[indexDate].Substring(6, 2));
        }

        if (fileName.Contains("dateFR") && fileName.Contains("slashed"))
        {
          annee = int.Parse(tmpNumbers[indexDate].Substring(6, 4));
          mois = int.Parse(tmpNumbers[indexDate].Substring(3, 2));
          jour = int.Parse(tmpNumbers[indexDate].Substring(0, 2));
        }

        Tirage unTirage = new Tirage
        {
          Boule1 = int.Parse(tmpNumbers[index]),
          Boule2 = int.Parse(tmpNumbers[index + 1]),
          Boule3 = int.Parse(tmpNumbers[index + 2]),
          Boule4 = int.Parse(tmpNumbers[index + 3]),
          Boule5 = int.Parse(tmpNumbers[index + 4]),
          Etoile1 = int.Parse(tmpNumbers[index + 5]),
          Etoile2 = int.Parse(tmpNumbers[index + 6]),
          DateTirage = new DateTime(annee, mois, jour)
        };

        result.Add(OrderTirage(unTirage));
      }

      return result;
    }

    private int GetPositionOfString(string[] oneArray, string stringToSearch)
    {
      int result = 0;
      for (int i = 0; i < oneArray.Length; i++)
      {
        if (oneArray[i] == stringToSearch)
        {
          result = i;
          break;
        }
      }

      return result;
    }

    public static Tirage OrderTirage(Tirage unTirage)
    {
      Tirage result = new Tirage
      {
        Etoile1 = unTirage.Etoile1,
        Etoile2 = unTirage.Etoile2,
        DateTirage = unTirage.DateTirage
      };

      int[] tmpNumbers = { unTirage.Boule1, unTirage.Boule2, unTirage.Boule3, unTirage.Boule4, unTirage.Boule5 };
      tmpNumbers = BubbleSort(tmpNumbers.ToList()).ToArray();
      result.Boule1 = tmpNumbers[0];
      result.Boule2 = tmpNumbers[1];
      result.Boule3 = tmpNumbers[2];
      result.Boule4 = tmpNumbers[3];
      result.Boule5 = tmpNumbers[4];
      return result;
    }

    public static List<int> BubbleSort(List<int> myList)
    {
      List<int> result = myList;
      while (true)
      {
        bool swapped = false;
        for (int i = 0; i < myList.Count - 1; ++i)
        {
          if (myList[i] > myList[i + 1])
          {
            int tmp = myList[i];
            myList[i] = myList[i + 1];
            myList[i + 1] = tmp;
            swapped = true;
          }
        }

        if (!swapped) { break; }
      }

      return result;
    }

    private static ColumnHeaderAutoResizeStyle GetLongestString(string headerText, ListView lv, int columnNumber)
    {
      return headerText.Length > MaxString(lv.Items, columnNumber).Length ? ColumnHeaderAutoResizeStyle.HeaderSize : ColumnHeaderAutoResizeStyle.ColumnContent;
    }

    private static string MaxString(ListView.ListViewItemCollection items, int columnNumber)
    {
      string longest = string.Empty;
      foreach (ListViewItem item in items) // items[columnNumber].SubItems
      {
        if (item.ToString().Length > longest.Length)
        {
          longest = item.Text;
        }
      }

      return longest;
    }

    private static bool IsInlistView(ListView listView, ListViewItem lviItem, int columnNumber)
    {
      bool result = false;
      foreach (ListViewItem item in listView.Items)
      {
        if (item.SubItems[columnNumber].Text == lviItem.SubItems[columnNumber].Text)
        {
          result = true;
          break;
        }
      }

      return result;
    }

    private void LoadConfigurationOptions()
    {
      // Add any other options here
      _configurationOptions.Option1Name = Settings.Default.Option1Name;
      _configurationOptions.Option2Name = Settings.Default.Option2Name;
    }

    private void SaveConfigurationOptions()
    {
      // Add any other options here
      _configurationOptions.Option1Name = Settings.Default.Option1Name;
      _configurationOptions.Option2Name = Settings.Default.Option2Name;
    }

    private void LoadLanguages()
    {
      if (!File.Exists(Settings.Default.LanguageFileName))
      {
        CreateLanguageFile();
      }

      // read the translation file and feed the language
      XDocument xDoc;
      try
      {
        xDoc = XDocument.Load(Settings.Default.LanguageFileName);
      }
      catch (Exception exception)
      {
        MessageBox.Show(Resources.Error_while_loading_the + Punctuation.OneSpace +
          Settings.Default.LanguageFileName + Punctuation.OneSpace + Resources.XML_file +
          Punctuation.OneSpace + exception.Message);
        CreateLanguageFile();
        return;
      }

      var result = from node in xDoc.Descendants("term")
                   where node.HasElements
                   let xElementName = node.Element("name")
                   where xElementName != null
                   let xElementEnglish = node.Element("englishValue")
                   where xElementEnglish != null
                   let xElementFrench = node.Element("frenchValue")
                   where xElementFrench != null
                   select new
                   {
                     name = xElementName.Value,
                     englishValue = xElementEnglish.Value,
                     frenchValue = xElementFrench.Value
                   };
      foreach (var i in result)
      {
        if (!_languageDicoEn.ContainsKey(i.name))
        {
          _languageDicoEn.Add(i.name, i.englishValue);
        }
#if DEBUG
        else
        {
          MessageBox.Show(Resources.Your_XML_file_has_duplicate_like + Punctuation.Colon +
            Punctuation.OneSpace + i.name);
        }
#endif
        if (!_languageDicoFr.ContainsKey(i.name))
        {
          _languageDicoFr.Add(i.name, i.frenchValue);
        }
#if DEBUG
        else
        {
          MessageBox.Show(Resources.Your_XML_file_has_duplicate_like + Punctuation.Colon +
            Punctuation.OneSpace + i.name);
        }
#endif
      }
    }

    private static void CreateLanguageFile()
    {
      List<string> minimumVersion = new List<string>
      {
        "<?xml version=\"1.0\" encoding=\"utf-8\" ?>",
        "<terms>",
         "<term>",
        "<name>MenuFile</name>",
        "<englishValue>File</englishValue>",
        "<frenchValue>Fichier</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileNew</name>",
        "<englishValue>New</englishValue>",
        "<frenchValue>Nouveau</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileOpen</name>",
        "<englishValue>Open</englishValue>",
        "<frenchValue>Ouvrir</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileSave</name>",
        "<englishValue>Save</englishValue>",
        "<frenchValue>Enregistrer</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileSaveAs</name>",
        "<englishValue>Save as ...</englishValue>",
        "<frenchValue>Enregistrer sous ...</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFilePrint</name>",
        "<englishValue>Print ...</englishValue>",
        "<frenchValue>Imprimer ...</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenufilePageSetup</name>",
          "<englishValue>Page setup</englishValue>",
          "<frenchValue>Aperçu avant impression</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenufileQuit</name>",
          "<englishValue>Quit</englishValue>",
          "<frenchValue>Quitter</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEdit</name>",
          "<englishValue>Edit</englishValue>",
          "<frenchValue>Edition</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditCancel</name>",
          "<englishValue>Cancel</englishValue>",
          "<frenchValue>Annuler</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditRedo</name>",
          "<englishValue>Redo</englishValue>",
          "<frenchValue>Rétablir</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditCut</name>",
          "<englishValue>Cut</englishValue>",
          "<frenchValue>Couper</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditCopy</name>",
          "<englishValue>Copy</englishValue>",
          "<frenchValue>Copier</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditPaste</name>",
          "<englishValue>Paste</englishValue>",
          "<frenchValue>Coller</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditSelectAll</name>",
          "<englishValue>Select All</englishValue>",
          "<frenchValue>Sélectionner tout</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuTools</name>",
          "<englishValue>Tools</englishValue>",
          "<frenchValue>Outils</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuToolsCustomize</name>",
          "<englishValue>Customize ...</englishValue>",
          "<frenchValue>Personaliser ...</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuToolsOptions</name>",
          "<englishValue>Options</englishValue>",
          "<frenchValue>Options</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuLanguage</name>",
          "<englishValue>Language</englishValue>",
          "<frenchValue>Langage</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuLanguageEnglish</name>",
          "<englishValue>English</englishValue>",
          "<frenchValue>Anglais</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuLanguageFrench</name>",
          "<englishValue>French</englishValue>",
          "<frenchValue>Français</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelp</name>",
          "<englishValue>Help</englishValue>",
          "<frenchValue>Aide</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpSummary</name>",
          "<englishValue>Summary</englishValue>",
          "<frenchValue>Sommaire</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpIndex</name>",
          "<englishValue>Index</englishValue>",
          "<frenchValue>Index</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpSearch</name>",
          "<englishValue>Search</englishValue>",
          "<frenchValue>Rechercher</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpAbout</name>",
          "<englishValue>About</englishValue>",
          "<frenchValue>A propos de ...</frenchValue>",
        "</term>",
        "</terms>"
      };

      StreamWriter sw = new StreamWriter(Settings.Default.LanguageFileName);
      foreach (string item in minimumVersion)
      {
        sw.WriteLine(item);
      }

      sw.Close();
    }

    private void GetWindowValue()
    {
      Width = Settings.Default.WindowWidth;
      Height = Settings.Default.WindowHeight;
      Top = Settings.Default.WindowTop < 0 ? 0 : Settings.Default.WindowTop;
      Left = Settings.Default.WindowLeft < 0 ? 0 : Settings.Default.WindowLeft;
      tabControlMain.SelectedIndex = Settings.Default.LatestTabUsed;
      SetDisplayOption(Settings.Default.DisplayToolStripMenuItem);
      LoadConfigurationOptions();
    }

    private void SaveWindowValue()
    {
      Settings.Default.WindowHeight = Height;
      Settings.Default.WindowWidth = Width;
      Settings.Default.WindowLeft = Left;
      Settings.Default.WindowTop = Top;
      Settings.Default.LastLanguageUsed = frenchToolStripMenuItem.Checked ? "French" : "English";
      Settings.Default.DisplayToolStripMenuItem = GetDisplayOption();
      Settings.Default.LatestTabUsed = tabControlMain.SelectedIndex;
      SaveConfigurationOptions();
      Settings.Default.Save();
    }

    private string GetDisplayOption()
    {
      if (SmallToolStripMenuItem.Checked)
      {
        return "Small";
      }

      if (MediumToolStripMenuItem.Checked)
      {
        return "Medium";
      }

      // Add any other options here
      return LargeToolStripMenuItem.Checked ? "Large" : string.Empty;
    }

    private void SetDisplayOption(string option)
    {
      UncheckAllOptions();
      switch (option.ToLower())
      {
        case "small":
          SmallToolStripMenuItem.Checked = true;
          break;
        case "medium":
          MediumToolStripMenuItem.Checked = true;
          break;
        case "large":
          LargeToolStripMenuItem.Checked = true;
          break;
        // Add any other options here
        default:
          SmallToolStripMenuItem.Checked = true;
          break;
      }
    }

    private void UncheckAllOptions()
    {
      // Add any other options here
      SmallToolStripMenuItem.Checked = false;
      MediumToolStripMenuItem.Checked = false;
      LargeToolStripMenuItem.Checked = false;
    }

    private void FormMainFormClosing(object sender, FormClosingEventArgs e)
    {
      // save all windows settings
      SaveWindowValue();
    }

    private void FrenchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _currentLanguage = Language.French.ToString();
      SetLanguage(Language.French.ToString());
      AdjustAllControls();
    }

    private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _currentLanguage = Language.English.ToString();
      SetLanguage(Language.English.ToString());
      AdjustAllControls();
    }

    private void SetLanguage(string myLanguage)
    {
      // Add any new language
      switch (myLanguage)
      {
        case "English":
          frenchToolStripMenuItem.Checked = false;
          englishToolStripMenuItem.Checked = true;
          fileToolStripMenuItem.Text = _languageDicoEn["MenuFile"];
          newToolStripMenuItem.Text = _languageDicoEn["MenuFileNew"];
          openToolStripMenuItem.Text = _languageDicoEn["MenuFileOpen"];
          saveToolStripMenuItem.Text = _languageDicoEn["MenuFileSave"];
          saveasToolStripMenuItem.Text = _languageDicoEn["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = _languageDicoEn["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = _languageDicoEn["MenufilePageSetup"];
          quitToolStripMenuItem.Text = _languageDicoEn["MenufileQuit"];
          editToolStripMenuItem.Text = _languageDicoEn["MenuEdit"];
          cancelToolStripMenuItem.Text = _languageDicoEn["MenuEditCancel"];
          redoToolStripMenuItem.Text = _languageDicoEn["MenuEditRedo"];
          cutToolStripMenuItem.Text = _languageDicoEn["MenuEditCut"];
          copyToolStripMenuItem.Text = _languageDicoEn["MenuEditCopy"];
          pasteToolStripMenuItem.Text = _languageDicoEn["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = _languageDicoEn["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = _languageDicoEn["MenuTools"];
          personalizeToolStripMenuItem.Text = _languageDicoEn["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = _languageDicoEn["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = _languageDicoEn["MenuLanguage"];
          englishToolStripMenuItem.Text = _languageDicoEn["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = _languageDicoEn["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = _languageDicoEn["MenuHelp"];
          summaryToolStripMenuItem.Text = _languageDicoEn["MenuHelpSummary"];
          indexToolStripMenuItem.Text = _languageDicoEn["MenuHelpIndex"];
          searchToolStripMenuItem.Text = _languageDicoEn["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = _languageDicoEn["MenuHelpAbout"];
          DisplayToolStripMenuItem.Text = _languageDicoEn["Display"];
          SmallToolStripMenuItem.Text = _languageDicoEn["Small"];
          MediumToolStripMenuItem.Text = _languageDicoEn["Medium"];
          LargeToolStripMenuItem.Text = _languageDicoEn["Large"];

          _currentLanguage = "English";
          break;
        case "French":
          frenchToolStripMenuItem.Checked = true;
          englishToolStripMenuItem.Checked = false;
          fileToolStripMenuItem.Text = _languageDicoFr["MenuFile"];
          newToolStripMenuItem.Text = _languageDicoFr["MenuFileNew"];
          openToolStripMenuItem.Text = _languageDicoFr["MenuFileOpen"];
          saveToolStripMenuItem.Text = _languageDicoFr["MenuFileSave"];
          saveasToolStripMenuItem.Text = _languageDicoFr["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = _languageDicoFr["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = _languageDicoFr["MenufilePageSetup"];
          quitToolStripMenuItem.Text = _languageDicoFr["MenufileQuit"];
          editToolStripMenuItem.Text = _languageDicoFr["MenuEdit"];
          cancelToolStripMenuItem.Text = _languageDicoFr["MenuEditCancel"];
          redoToolStripMenuItem.Text = _languageDicoFr["MenuEditRedo"];
          cutToolStripMenuItem.Text = _languageDicoFr["MenuEditCut"];
          copyToolStripMenuItem.Text = _languageDicoFr["MenuEditCopy"];
          pasteToolStripMenuItem.Text = _languageDicoFr["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = _languageDicoFr["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = _languageDicoFr["MenuTools"];
          personalizeToolStripMenuItem.Text = _languageDicoFr["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = _languageDicoFr["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = _languageDicoFr["MenuLanguage"];
          englishToolStripMenuItem.Text = _languageDicoFr["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = _languageDicoFr["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = _languageDicoFr["MenuHelp"];
          summaryToolStripMenuItem.Text = _languageDicoFr["MenuHelpSummary"];
          indexToolStripMenuItem.Text = _languageDicoFr["MenuHelpIndex"];
          searchToolStripMenuItem.Text = _languageDicoFr["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = _languageDicoFr["MenuHelpAbout"];
          DisplayToolStripMenuItem.Text = _languageDicoFr["Display"];
          SmallToolStripMenuItem.Text = _languageDicoFr["Small"];
          MediumToolStripMenuItem.Text = _languageDicoFr["Medium"];
          LargeToolStripMenuItem.Text = _languageDicoFr["Large"];

          _currentLanguage = "French";
          break;
        default:
          SetLanguage("English");
          break;
      }
    }

    private void CutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control>());
      var tb = focusedControl as TextBox;
      if (tb != null)
      {
        CutToClipboard(tb);
      }
    }

    private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control>());
      var tb = focusedControl as TextBox;
      if (tb != null)
      {
        CopyToClipboard(tb);
      }
    }

    private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control>());
      var tb = focusedControl as TextBox;
      if (tb != null)
      {
        PasteFromClipboard(tb);
      }
    }

    private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control>());
      TextBox control = focusedControl as TextBox;
      if (control != null) control.SelectAll();
    }

    private void CutToClipboard(TextBoxBase tb, string errorMessage = "nothing")
    {
      if (tb != ActiveControl)
      {
        return;
      }

      if (tb.Text == string.Empty)
      {
        DisplayMessage(Translate("ThereIs") + Punctuation.OneSpace +
          Translate(errorMessage) + Punctuation.OneSpace +
          Translate("ToCut") + Punctuation.OneSpace, Translate(errorMessage),
          MessageBoxButtons.OK);
        return;
      }

      if (tb.SelectedText == string.Empty)
      {
        DisplayMessage(Translate("NoTextHasBeenSelected"),
          Translate(errorMessage), MessageBoxButtons.OK);
        return;
      }

      Clipboard.SetText(tb.SelectedText);
      tb.SelectedText = string.Empty;
    }

    private void CopyToClipboard(TextBoxBase tb, string message = "nothing")
    {
      if (tb != ActiveControl)
      {
        return;
      }

      if (tb.Text == string.Empty)
      {
        DisplayMessage(Translate("ThereIsNothingToCopy") + Punctuation.OneSpace,
          Translate(message), MessageBoxButtons.OK);
        return;
      }

      if (tb.SelectedText == string.Empty)
      {
        DisplayMessage(Translate("NoTextHasBeenSelected"),
          Translate(message), MessageBoxButtons.OK);
        return;
      }

      Clipboard.SetText(tb.SelectedText);
    }

    private void PasteFromClipboard(TextBoxBase tb)
    {
      if (tb != ActiveControl)
      {
        return;
      }

      var selectionIndex = tb.SelectionStart;
      tb.SelectedText = Clipboard.GetText();
      tb.SelectionStart = selectionIndex + Clipboard.GetText().Length;
    }

    private void DisplayMessage(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.OK)
    {
      MessageBox.Show(this, message, title, buttons);
    }

    private string Translate(string word)
    {
      string result = string.Empty;
      switch (_currentLanguage.ToLower())
      {
        case "english":
          result = _languageDicoEn.ContainsKey(word) ? _languageDicoEn[word] :
           "the term: \"" + word + "\" has not been translated yet.\nPlease tell the developer to translate this term";
          break;
        case "french":
          result = _languageDicoFr.ContainsKey(word) ? _languageDicoFr[word] :
            "the term: \"" + word + "\" has not been translated yet.\nPlease tell the developer to translate this term";
          break;
      }

      return result;
    }

    private static Control FindFocusedControl(Control container)
    {
      foreach (Control childControl in container.Controls.Cast<Control>().Where(childControl => childControl.Focused))
      {
        return childControl;
      }

      return (from Control childControl in container.Controls
              select FindFocusedControl(childControl)).FirstOrDefault(maybeFocusedControl => maybeFocusedControl != null);
    }

    private static Control FindFocusedControl(List<Control> container)
    {
      return container.FirstOrDefault(control => control.Focused);
    }

    private static Control FindFocusedControl(params Control[] container)
    {
      return container.FirstOrDefault(control => control.Focused);
    }

    private static Control FindFocusedControl(IEnumerable<Control> container)
    {
      return container.FirstOrDefault(control => control.Focused);
    }

    public static string PeekDirectory()
    {
      string result = string.Empty;
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      if (fbd.ShowDialog() == DialogResult.OK)
      {
        result = fbd.SelectedPath;
      }

      return result;
    }

    public static string PeekFile()
    {
      string result = string.Empty;
      OpenFileDialog fd = new OpenFileDialog();
      if (fd.ShowDialog() == DialogResult.OK)
      {
        result = fd.SafeFileName;
      }

      return result;
    }

    private void SmallToolStripMenuItemClick(object sender, EventArgs e)
    {
      UncheckAllOptions();
      SmallToolStripMenuItem.Checked = true;
      AdjustAllControls();
    }

    private void MediumToolStripMenuItemClick(object sender, EventArgs e)
    {
      UncheckAllOptions();
      MediumToolStripMenuItem.Checked = true;
      AdjustAllControls();
    }

    private void LargeToolStripMenuItemClick(object sender, EventArgs e)
    {
      UncheckAllOptions();
      LargeToolStripMenuItem.Checked = true;
      AdjustAllControls();
    }

    private static void AdjustControls(int initialPadding = 33, params Control[] listOfControls)
    {
      if (listOfControls.Length == 0)
      {
        return;
      }

      int position = listOfControls[0].Width + initialPadding; // 33 is the initial padding
      bool isFirstControl = true;
      foreach (Control control in listOfControls)
      {
        if (isFirstControl)
        {
          isFirstControl = false;
        }
        else
        {
          control.Left = position + 10;
          position += control.Width;
        }
      }
    }

    private void AdjustAllControls()
    {
      // insert here all labels, textboxes and buttons, one method per line of controls
      AdjustControls();
    }

    private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FormOptions frmOptions = new FormOptions(_configurationOptions);

      if (frmOptions.ShowDialog() == DialogResult.OK)
      {
        _configurationOptions = frmOptions.ConfigurationOptions2;
      }
    }

    private static void SetButtonEnabled(Button button, params Control[] controls)
    {
      bool result = true;
      foreach (Control ctrl in controls)
      {
        if (ctrl.GetType() == typeof(TextBox))
        {
          if (((TextBox)ctrl).Text == string.Empty)
          {
            result = false;
            break;
          }
        }

        if (ctrl.GetType() == typeof(ListView))
        {
          if (((ListView)ctrl).Items.Count == 0)
          {
            result = false;
            break;
          }
        }

        if (ctrl.GetType() == typeof(ComboBox))
        {
          if (((ComboBox)ctrl).SelectedIndex == -1)
          {
            result = false;
            break;
          }
        }
      }

      button.Enabled = result;
    }

    private void TextBoxName_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        // do something
      }
    }

    private void ButtonFlashGenerer_Click(object sender, EventArgs e)
    {
      buttonFlashGenerer.Enabled = false;
      Application.DoEvents();
      // clear grid
      var unTirage = new Tirage();

      // generate 5 new balls
      var numbers = HelperClasses.GenerateSeveralRandomNumbers(1, 50, 5);
      unTirage.AssigneBoules(numbers);

      // generate 2 new stars
      var etoiles = HelperClasses.GenerateSeveralRandomNumbers(1, 12, 2);
      unTirage.AssigneEtoiles(etoiles);

      // clear all balls and stars
      ClearAllBalls();
      ClearAllStars();

      // Highlight selected balls
      foreach (int number in numbers)
      {
        HighlightBall(number);
      }

      // Highlight selected stars
      foreach (int star in etoiles)
      {
        HighlightStar(star);
      }

      labelFlashPoidsBoule.Text = "Poids des boules : " + unTirage.PoidsDesBoules();
      labelFlashBallsShouldBeBetween.Text = $"Poids entre 15 et 240. Moyenne théorique = 112. Moyenne réelle = {listTirages.MediumWeigh()}. Median = {listTirages.MedianWeigh()}";
      labelFlashPoidsDesEtoiles.Text = "Poids des étoiles : " + unTirage.PoidsDesEtoiles();
      labelFlashPoidsEtoilesEntre.Text = "Poids entre 3 et 23. Moyenne = 10. Median = X";
      labelFlashNombreParLigne.Text = "Nombre par ligne : " + Tirage.NombreParLigne(Tirage.RepartitionDizaineTirage(unTirage));
      labelFlashNombreParColonne.Text = "Nombre par colonne : " + Tirage.NombreParColonne(Tirage.RepartitionParColonne(unTirage));
      // 5 boules déjà sortis
      labelFlash5BoulesdejaSortis.Text = "5 boules déjà sortis : " + unTirage.ToString();
      bool FiveBallsAlreadyDrawn = AlreadyDrawn5Balls(unTirage);
      labelFlash5BoulesDejaSortisResult.Text = StringifyYesNo(FiveBallsAlreadyDrawn);
      labelFlash5BoulesDejaSortisResult.BackColor = ColorizeYesNo(FiveBallsAlreadyDrawn);

      // 4 boules déjà sortis
      labelFlash4BoulesDejaSortis.Text = "4 boules déjà sortis : ";
      var result4Balls = AlreadyDrawn4Balls(unTirage);
      if (result4Balls[0] == "Oui")
      {
        labelFlash4BoulesDejaSortis.Text += result4Balls[1];
        labelFlash4BoulesDejaSortisResult.Text = "Oui";
        labelFlash4BoulesDejaSortisResult.BackColor = Color.OrangeRed;
      }
      else
      {
        labelFlash4BoulesDejaSortisResult.Text = "Non";
        labelFlash4BoulesDejaSortisResult.BackColor = Color.LightGreen;
      }

      var test = listTirages;
      buttonFlashGenerer.Enabled = true;
    }

    private Color ColorizeYesNo(bool booleanValue)
    {
      return booleanValue ? Color.OrangeRed : Color.LightGreen;
    }

    private string[] AlreadyDrawn4Balls(Tirage tirageRecherche)
    {
      bool resultFound = false;
      string[] finalResult = new string[2];
      finalResult[0] = "Non";
      finalResult[1] = "";

      foreach (Tirage tirage in listTirages.ListOfTirages)
      {
        if (tirageRecherche.FirstFourToString() == tirage.FirstFourToString())
        {
          // out 5
          resultFound = true;
          finalResult[1] = tirage.FirstFourToString();
          break;
        }
        else if (tirageRecherche.LastFourToString() == tirage.LastFourToString())
        {
          // out 1
          resultFound = true;
          finalResult[1] = tirage.LastFourToString();
          break;
        }
        else if (tirageRecherche.Boule1 == tirage.Boule1 &&
          tirageRecherche.Boule2 == tirage.Boule2 &&
          tirageRecherche.Boule3 == tirage.Boule3 &&
          tirageRecherche.Boule5 == tirage.Boule5)
        {
          // out 4
          resultFound = true;
          finalResult[1] = $"{tirage.Boule1}-{tirage.Boule2}-{tirage.Boule3}-{tirage.Boule5}";
          break;
        }
        else if (tirageRecherche.Boule1 == tirage.Boule1 &&
          tirageRecherche.Boule3 == tirage.Boule3 &&
          tirageRecherche.Boule4 == tirage.Boule4 &&
          tirageRecherche.Boule5 == tirage.Boule5)
        {
          // out 2
          resultFound = true;
          finalResult[1] = $"{tirage.Boule1}-{tirage.Boule3}-{tirage.Boule4}-{tirage.Boule5}";
          break;
        }
        else if (tirageRecherche.Boule1 == tirage.Boule1 &&
          tirageRecherche.Boule2 == tirage.Boule2 &&
          tirageRecherche.Boule4 == tirage.Boule4 &&
          tirageRecherche.Boule5 == tirage.Boule5)
        {
          // out 3
          resultFound = true;
          finalResult[1] = $"{tirage.Boule1}-{tirage.Boule2}-{tirage.Boule4}-{tirage.Boule5}";
          break;
        }
      }

      finalResult[0] = StringifyYesNo(resultFound);
      return finalResult;
    }

    private bool AlreadyDrawn5Balls(Tirage tirageRecherche)
    {
      bool resultFound = false;

      foreach (Tirage tirage in listTirages.ListOfTirages)
      {
        if (tirageRecherche.ToString() == tirage.ToString())
        {
          resultFound = true;
          break;
        }
      }

      return resultFound;
    }

    private static string StringifyYesNo(bool booleanValue)
    {
      return booleanValue ? "Oui" : "Non";
    }

    private void HighlightStar(int number)
    {
      switch (number)
      {
        case 1:
          buttonFlashEtoile1.BackColor = Color.OrangeRed;
          break;
        case 2:
          buttonFlashEtoile2.BackColor = Color.OrangeRed;
          break;
        case 3:
          buttonFlashEtoile3.BackColor = Color.OrangeRed;
          break;
        case 4:
          buttonFlashEtoile4.BackColor = Color.OrangeRed;
          break;
        case 5:
          buttonFlashEtoile5.BackColor = Color.OrangeRed;
          break;
        case 6:
          buttonFlashEtoile6.BackColor = Color.OrangeRed;
          break;
        case 7:
          buttonFlashEtoile7.BackColor = Color.OrangeRed;
          break;
        case 8:
          buttonFlashEtoile8.BackColor = Color.OrangeRed;
          break;
        case 9:
          buttonFlashEtoile9.BackColor = Color.OrangeRed;
          break;
        case 10:
          buttonFlashEtoile10.BackColor = Color.OrangeRed;
          break;
        case 11:
          buttonFlashEtoile11.BackColor = Color.OrangeRed;
          break;
        case 12:
          buttonFlashEtoile12.BackColor = Color.OrangeRed;
          break;
        default:
          break;
      }
    }

    private void HighlightBall(int number)
    {
      switch (number)
      {
        case 1:
          buttonFlashBoule1.BackColor = Color.OrangeRed;
          break;
        case 2:
          buttonFlashBoule2.BackColor = Color.OrangeRed;
          break;
        case 3:
          buttonFlashBoule3.BackColor = Color.OrangeRed;
          break;
        case 4:
          buttonFlashBoule4.BackColor = Color.OrangeRed;
          break;
        case 5:
          buttonFlashBoule5.BackColor = Color.OrangeRed;
          break;
        case 6:
          buttonFlashBoule6.BackColor = Color.OrangeRed;
          break;
        case 7:
          buttonFlashBoule7.BackColor = Color.OrangeRed;
          break;
        case 8:
          buttonFlashBoule8.BackColor = Color.OrangeRed;
          break;
        case 9:
          buttonFlashBoule9.BackColor = Color.OrangeRed;
          break;
        case 10:
          buttonFlashBoule10.BackColor = Color.OrangeRed;
          break;
        case 11:
          buttonFlashBoule11.BackColor = Color.OrangeRed;
          break;
        case 12:
          buttonFlashBoule12.BackColor = Color.OrangeRed;
          break;
        case 13:
          buttonFlashBoule13.BackColor = Color.OrangeRed;
          break;
        case 14:
          buttonFlashBoule14.BackColor = Color.OrangeRed;
          break;
        case 15:
          buttonFlashBoule15.BackColor = Color.OrangeRed;
          break;
        case 16:
          buttonFlashBoule16.BackColor = Color.OrangeRed;
          break;
        case 17:
          buttonFlashBoule17.BackColor = Color.OrangeRed;
          break;
        case 18:
          buttonFlashBoule18.BackColor = Color.OrangeRed;
          break;
        case 19:
          buttonFlashBoule19.BackColor = Color.OrangeRed;
          break;
        case 20:
          buttonFlashBoule20.BackColor = Color.OrangeRed;
          break;
        case 21:
          buttonFlashBoule21.BackColor = Color.OrangeRed;
          break;
        case 22:
          buttonFlashBoule22.BackColor = Color.OrangeRed;
          break;
        case 23:
          buttonFlashBoule23.BackColor = Color.OrangeRed;
          break;
        case 24:
          buttonFlashBoule24.BackColor = Color.OrangeRed;
          break;
        case 25:
          buttonFlashBoule25.BackColor = Color.OrangeRed;
          break;
        case 26:
          buttonFlashBoule26.BackColor = Color.OrangeRed;
          break;
        case 27:
          buttonFlashBoule27.BackColor = Color.OrangeRed;
          break;
        case 28:
          buttonFlashBoule28.BackColor = Color.OrangeRed;
          break;
        case 29:
          buttonFlashBoule29.BackColor = Color.OrangeRed;
          break;
        case 30:
          buttonFlashBoule30.BackColor = Color.OrangeRed;
          break;
        case 31:
          buttonFlashBoule31.BackColor = Color.OrangeRed;
          break;
        case 32:
          buttonFlashBoule32.BackColor = Color.OrangeRed;
          break;
        case 33:
          buttonFlashBoule33.BackColor = Color.OrangeRed;
          break;
        case 34:
          buttonFlashBoule34.BackColor = Color.OrangeRed;
          break;
        case 35:
          buttonFlashBoule35.BackColor = Color.OrangeRed;
          break;
        case 36:
          buttonFlashBoule36.BackColor = Color.OrangeRed;
          break;
        case 37:
          buttonFlashBoule37.BackColor = Color.OrangeRed;
          break;
        case 38:
          buttonFlashBoule38.BackColor = Color.OrangeRed;
          break;
        case 39:
          buttonFlashBoule39.BackColor = Color.OrangeRed;
          break;
        case 40:
          buttonFlashBoule40.BackColor = Color.OrangeRed;
          break;
        case 41:
          buttonFlashBoule41.BackColor = Color.OrangeRed;
          break;
        case 42:
          buttonFlashBoule42.BackColor = Color.OrangeRed;
          break;
        case 43:
          buttonFlashBoule43.BackColor = Color.OrangeRed;
          break;
        case 44:
          buttonFlashBoule44.BackColor = Color.OrangeRed;
          break;
        case 45:
          buttonFlashBoule45.BackColor = Color.OrangeRed;
          break;
        case 46:
          buttonFlashBoule46.BackColor = Color.OrangeRed;
          break;
        case 47:
          buttonFlashBoule47.BackColor = Color.OrangeRed;
          break;
        case 48:
          buttonFlashBoule48.BackColor = Color.OrangeRed;
          break;
        case 49:
          buttonFlashBoule49.BackColor = Color.OrangeRed;
          break;
        case 50:
          buttonFlashBoule50.BackColor = Color.OrangeRed;
          break;
        default:
          break;
      }
    }

    private void ClearAllStars()
    {
      buttonFlashEtoile1.BackColor = Color.Transparent;
      buttonFlashEtoile2.BackColor = Color.Transparent;
      buttonFlashEtoile3.BackColor = Color.Transparent;
      buttonFlashEtoile4.BackColor = Color.Transparent;
      buttonFlashEtoile5.BackColor = Color.Transparent;
      buttonFlashEtoile6.BackColor = Color.Transparent;
      buttonFlashEtoile7.BackColor = Color.Transparent;
      buttonFlashEtoile8.BackColor = Color.Transparent;
      buttonFlashEtoile9.BackColor = Color.Transparent;
      buttonFlashEtoile10.BackColor = Color.Transparent;
      buttonFlashEtoile11.BackColor = Color.Transparent;
      buttonFlashEtoile12.BackColor = Color.Transparent;
    }

    private void ClearAllBalls()
    {
      buttonFlashBoule1.BackColor = Color.Transparent;
      buttonFlashBoule2.BackColor = Color.Transparent;
      buttonFlashBoule3.BackColor = Color.Transparent;
      buttonFlashBoule4.BackColor = Color.Transparent;
      buttonFlashBoule5.BackColor = Color.Transparent;
      buttonFlashBoule6.BackColor = Color.Transparent;
      buttonFlashBoule7.BackColor = Color.Transparent;
      buttonFlashBoule8.BackColor = Color.Transparent;
      buttonFlashBoule9.BackColor = Color.Transparent;
      buttonFlashBoule10.BackColor = Color.Transparent;
      buttonFlashBoule11.BackColor = Color.Transparent;
      buttonFlashBoule12.BackColor = Color.Transparent;
      buttonFlashBoule13.BackColor = Color.Transparent;
      buttonFlashBoule14.BackColor = Color.Transparent;
      buttonFlashBoule15.BackColor = Color.Transparent;
      buttonFlashBoule16.BackColor = Color.Transparent;
      buttonFlashBoule17.BackColor = Color.Transparent;
      buttonFlashBoule18.BackColor = Color.Transparent;
      buttonFlashBoule19.BackColor = Color.Transparent;
      buttonFlashBoule20.BackColor = Color.Transparent;
      buttonFlashBoule21.BackColor = Color.Transparent;
      buttonFlashBoule22.BackColor = Color.Transparent;
      buttonFlashBoule23.BackColor = Color.Transparent;
      buttonFlashBoule24.BackColor = Color.Transparent;
      buttonFlashBoule25.BackColor = Color.Transparent;
      buttonFlashBoule26.BackColor = Color.Transparent;
      buttonFlashBoule27.BackColor = Color.Transparent;
      buttonFlashBoule28.BackColor = Color.Transparent;
      buttonFlashBoule29.BackColor = Color.Transparent;
      buttonFlashBoule30.BackColor = Color.Transparent;
      buttonFlashBoule31.BackColor = Color.Transparent;
      buttonFlashBoule32.BackColor = Color.Transparent;
      buttonFlashBoule33.BackColor = Color.Transparent;
      buttonFlashBoule34.BackColor = Color.Transparent;
      buttonFlashBoule35.BackColor = Color.Transparent;
      buttonFlashBoule36.BackColor = Color.Transparent;
      buttonFlashBoule37.BackColor = Color.Transparent;
      buttonFlashBoule38.BackColor = Color.Transparent;
      buttonFlashBoule39.BackColor = Color.Transparent;
      buttonFlashBoule40.BackColor = Color.Transparent;
      buttonFlashBoule41.BackColor = Color.Transparent;
      buttonFlashBoule42.BackColor = Color.Transparent;
      buttonFlashBoule43.BackColor = Color.Transparent;
      buttonFlashBoule44.BackColor = Color.Transparent;
      buttonFlashBoule45.BackColor = Color.Transparent;
      buttonFlashBoule46.BackColor = Color.Transparent;
      buttonFlashBoule47.BackColor = Color.Transparent;
      buttonFlashBoule48.BackColor = Color.Transparent;
      buttonFlashBoule49.BackColor = Color.Transparent;
      buttonFlashBoule50.BackColor = Color.Transparent;
    }

    private void ButtonFlashBoule1_Click(object sender, EventArgs e)
    {
      if (buttonFlashBoule1.BackColor == Color.OrangeRed)
      {
        buttonFlashBoule1.BackColor = Color.Transparent;
      }
      else
      {
        buttonFlashBoule1.BackColor = Color.OrangeRed;
      }
    }

    private void ButtonFlashBoule2_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule3_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule4_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule5_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule6_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule7_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule8_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule9_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule10_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule11_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule39_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule29_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule19_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule50_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule48_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule47_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule46_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule45_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule44_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule43_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule42_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule41_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule40_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule38_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule37_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule36_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule35_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule34_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule33_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule32_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule31_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule30_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule28_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule27_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule26_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule25_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule24_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule23_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule22_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule21_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule20_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule18_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule17_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule16_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule15_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule14_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule13_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule12_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashBoule49_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile1_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile9_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile8_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile7_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile6_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile5_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile4_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile3_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile2_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile10_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile11_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashEtoile12_Click(object sender, EventArgs e)
    {

    }

    private void ButtonFlashGenerateOptimized_Click(object sender, EventArgs e)
    {
      // clear grid
      var unTirage = new Tirage();

      // generate 5 new balls
      var numbers = HelperClasses.GenerateSeveralRandomNumbers(1, 50, 5);
      unTirage.AssigneBoules(numbers);
      while (!IsOptimum(unTirage))
      {
        numbers = HelperClasses.GenerateSeveralRandomNumbers(1, 50, 5);
        unTirage.AssigneBoules(numbers);
      }

      // generate 2 new stars
      var etoiles = HelperClasses.GenerateSeveralRandomNumbers(1, 12, 2);
      unTirage.AssigneEtoiles(etoiles);

      // clear all balls and stars
      ClearAllBalls();
      ClearAllStars();

      // Highlight selected balls
      foreach (int number in numbers)
      {
        HighlightBall(number);
      }

      // Highlight selected stars
      foreach (int star in etoiles)
      {
        HighlightStar(star);
      }
    }

    private bool IsOptimum(Tirage tirage)
    {
      bool result = true;
      // rules
      int poids = tirage.PoidsDesBoules();

      return result;
    }

    private int NumberOfBallsSelected(params Control[] controls)
    {
      int result = 0;
      if (buttonFlashBoule1.BackColor == Color.OrangeRed)
      {
        result++;
      }

      foreach (Control ctrl in controls)
      {
        if (ctrl.GetType() == typeof(Button))
        {
          if (((Button)ctrl).Name.StartsWith("buttonFlashBoule"))
          {
            result++;
            break;
          }
        }
      }

      return result;
    }

    private static void SetBackColorButton(Color colour, params Control[] controls)
    {
      foreach (Control ctrl in controls)
      {
        if (ctrl.GetType() == typeof(Button))
        {
          if (((Button)ctrl).Name.StartsWith("buttonFlashBoule"))
          {
            ctrl.BackColor = colour;
          }
        }
      }
    }
  }
}

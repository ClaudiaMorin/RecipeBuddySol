using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RecipeBuddy.ViewModels;
using RecipeBuddy.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for Selected.xaml
    /// </summary>
    public partial class SelectedView : Page
    {
        public SelectedView()
        {

            InitializeComponent();
            //DetailsPanelForSelected.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            //Title.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            //DetailsPanelForSelected.NumXComboBox.DataContext = SelectedViewModel.Instance;
            //ControlsForSelectView.DataContext = SelectedViewModel.Instance;
            ////Wires the combobox to the list of recipes 
            RecipesInComboBox.ItemsSource = SelectedViewModel.Instance.listOfRecipeModel.RecipesList;

            //DetailsPanelForSelected.EditRow2.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox2.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant2BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd2.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd2.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd2.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow3.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox3.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant3BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd3.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd3.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd3.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow4.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox4.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant4BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd4.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd4.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd4.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow5.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox5.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant5BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd5.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd5.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd5.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow6.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox6.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant6BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd6.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd6.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd6.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow7.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox7.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant7BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd7.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd7.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd7.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow8.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox8.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant8BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd8.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd8.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd8.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow9.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox9.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant9BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd9.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd9.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd9.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow10.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox10.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant10BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd10.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd10.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd10.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow11.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox11.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant11BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd11.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd11.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd11.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow12.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox12.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant12BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd12.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd12.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd12.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow13.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox13.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant13BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd13.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd13.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd13.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow14.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox14.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant14BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd14.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd14.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd14.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow15.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox15.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant15BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd15.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd15.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd15.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow16.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox16.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant16BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd16.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd16.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd16.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow17.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox17.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant17BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd17.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd17.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd17.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow18.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox18.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant18BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd18.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd18.Command = SelectedViewModel.Instance.CmdLineEdit; 
            //DetailsPanelForSelected.CancelBtnIngrd18.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow19.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox19.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant19BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd19.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd19.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd19.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow20.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox20.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant20BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd20.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd20.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd20.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow21.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox21.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant21BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd21.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd21.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd21.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow22.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox22.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant22BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd22.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd22.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd22.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow23.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox23.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant23BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd23.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd23.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd23.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow24.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox24.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant24BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd24.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd24.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd24.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow25.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox25.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant25BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd25.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd25.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd25.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow26.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox26.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant26BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd26.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd26.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd26.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow27.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox27.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant27BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd27.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd27.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd27.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow28.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox28.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant28BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd28.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd28.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd28.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow29.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox29.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant29BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd29.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd29.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd29.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow30.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox30.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant30BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd30.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd30.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd30.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow31.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox31.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant31BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd31.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd31.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd31.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow32.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox32.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant32BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd32.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd32.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd32.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow33.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox33.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant33BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd33.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd33.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd33.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow34.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox34.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant34BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd34.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd34.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd34.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow35.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox35.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant35BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd35.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd35.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd35.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow36.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox36.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant36BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd36.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd36.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd36.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow37.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox37.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant37BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd37.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd37.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd37.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow38.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox38.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant38BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd38.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd38.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd38.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow39.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox39.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant39BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd39.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd39.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd39.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow40.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox40.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant40BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd40.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd40.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd40.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow41.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox41.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant41BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd41.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd41.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd41.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow42.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox42.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant42BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd42.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd42.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd42.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow43.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox43.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant43BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd43.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd43.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd43.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow44.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox44.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant44BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd44.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd44.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd44.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow45.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox45.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant45BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd45.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd45.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd45.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow46.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox46.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant46BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd46.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd46.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd46.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow47.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox47.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant47BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd47.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd47.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd47.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow48.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox48.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant48BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd48.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd48.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd48.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow49.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox49.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant49BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd49.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd49.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd49.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow50.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBox50.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.IngredQuant50BLK.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.UpdateBtnIngrd50.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd50.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd50.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.EditRow2Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir2.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect2.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect2.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect2.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow3Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir3.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect3.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect3.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect3.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow4Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir4.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect4.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect4.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect4.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow5Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir5.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect5.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect5.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect5.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow6Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir6.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect6.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect6.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect6.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow7Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir7.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect7.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect7.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect7.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow8Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir8.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect8.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect8.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect8.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow9Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir9.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect9.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect9.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect9.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow10Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir10.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect10.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect10.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect10.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow11Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir11.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect11.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect11.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect11.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow12Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir12.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect12.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect12.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect12.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow13Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir13.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect13.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect13.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect13.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow14Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir14.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect14.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect14.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect14.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow15Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir15.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect15.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect15.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect15.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow16Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir16.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect16.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect16.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect16.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow17Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir17.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect17.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect17.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect17.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow18Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir18.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect18.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect18.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect18.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow19Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir19.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect19.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect19.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect19.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow20Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir20.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect20.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect20.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect20.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow21Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir21.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect21.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect21.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect21.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow22Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir22.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect22.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect22.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect22.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow23Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir23.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect23.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect23.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect23.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow24Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir24.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect24.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect24.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect24.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow25Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir25.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect25.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect25.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect25.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow26Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir26.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect26.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect26.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect26.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow27Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir27.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect27.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect27.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect27.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow28Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir28.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect28.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect28.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect28.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow29Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir29.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect29.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect29.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect29.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.EditRow30Direct.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.TextBoxDir30.DataContext = SelectedViewModel.Instance;
            //DetailsPanelForSelected.EditBtnDirect30.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.UpdateBtnDirect30.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect30.Command = SelectedViewModel.Instance.CmdCancel;
        }
    }
}

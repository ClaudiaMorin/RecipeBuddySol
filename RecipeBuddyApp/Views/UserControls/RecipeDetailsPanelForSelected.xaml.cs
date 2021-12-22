using RecipeBuddy.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailsPanelForMakeIt.xaml
    /// </summary>
    public partial class RecipeDetailsPanelForSelected : UserControl
    {

        public RecipeDetailsPanelForSelected()
        {
            InitializeComponent();

            //IngredientGrid.DataContext = SelectedViewModel.Instance;
            //DirectionGrid.DataContext = SelectedViewModel.Instance;

            Author.DataContext = SelectedViewModel.Instance.SelectViewMainRecipeCardModel;
            Type.DataContext = SelectedViewModel.Instance.SelectViewMainRecipeCardModel;

            Ingred1.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred2.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred3.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred4.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred5.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred6.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred7.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred8.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred9.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred10.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred11.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred12.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred13.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred14.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred15.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred16.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred17.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred18.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred19.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred20.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred21.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred22.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred23.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred24.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred25.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred26.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred27.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred28.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred29.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred30.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred31.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred32.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred33.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred34.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred35.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred36.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred37.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred38.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred39.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred40.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred41.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred42.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred43.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred44.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred45.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred46.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred47.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred48.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred49.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred50.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;

            Dir1.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir2.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir3.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir4.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir5.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir6.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir7.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir8.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir9.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir10.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir11.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir12.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir13.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir14.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir15.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir16.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir17.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir18.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir19.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir20.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir21.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir22.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir23.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir24.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir25.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir26.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir27.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir28.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir29.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir30.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;



            //IngredQuant1BLK.DataContext = SelectedViewModel.Instance; 
            //EditBtnIngrd1.DataContext = SelectedViewModel.Instance;
            //TextBox1.DataContext = SelectedViewModel.Instance;
            //UpdateBtnIngrd1.DataContext = SelectedViewModel.Instance;
            //CancelBtnIngrd1.DataContext = SelectedViewModel.Instance;

            //UpdateBtnIngrd2.Command = SelectedViewModel.Instance.CmdUpdate;
            //EditBtnIngrd2.Command = SelectedViewModel.Instance.CmdLineEdit;
            //CancelBtnIngrd2.Command = SelectedViewModel.Instance.CmdUpdate;

            //UpdateBtnIngrd3.Command = SelectedViewModel.Instance.CmdUpdate;
            //EditBtnIngrd3.Command = SelectedViewModel.Instance.CmdLineEdit;
            //CancelBtnIngrd3.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd4.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd4.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd4.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd5.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd5.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd5.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd6.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd6.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd6.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd7.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd7.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd7.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd8.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd8.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd8.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd9.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd9.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd9.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd10.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd10.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd10.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd11.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd11.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd11.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd12.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd12.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd12.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd13.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd13.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd13.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd14.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd14.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd14.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd15.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd15.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd15.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd16.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd16.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd16.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd17.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd17.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd17.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd18.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd18.Command = SelectedViewModel.Instance.CmdLineEdit; 
            //DetailsPanelForSelected.CancelBtnIngrd18.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd19.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd19.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd19.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd20.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd20.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd20.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd21.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd21.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd21.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd22.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd22.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd22.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd23.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd23.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd23.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd24.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd24.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd24.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd25.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd25.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd25.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd26.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd26.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd26.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd27.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd27.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd27.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd28.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd28.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd28.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd29.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd29.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd29.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd30.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd30.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd30.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd31.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd31.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd31.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd32.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd32.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd32.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd33.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd33.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd33.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd34.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd34.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd34.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd35.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd35.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd35.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd36.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd36.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd36.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd37.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd37.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd37.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd38.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd38.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd38.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd39.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd39.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd39.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd40.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd40.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd40.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd41.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd41.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd41.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd42.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd42.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd42.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd43.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd43.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd43.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd44.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd44.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd44.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd45.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd45.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd45.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd46.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd46.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd46.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd47.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd47.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd47.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd48.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd48.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd48.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd49.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd49.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd49.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnIngrd50.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.EditBtnIngrd50.Command = SelectedViewModel.Instance.CmdLineEdit;
            //DetailsPanelForSelected.CancelBtnIngrd50.Command = SelectedViewModel.Instance.CmdUpdate;

            //DetailsPanelForSelected.UpdateBtnDirect2.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect2.Command = SelectedViewModel.Instance.CmdCancel;

            //DetailsPanelForSelected.UpdateBtnDirect3.Command = SelectedViewModel.Instance.CmdUpdate;
            //DetailsPanelForSelected.CancelBtnDirect3.Command = SelectedViewModel.Instance.CmdCancel;

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

        //private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //    Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        //    e.Handled = true;
        //}
    }
}

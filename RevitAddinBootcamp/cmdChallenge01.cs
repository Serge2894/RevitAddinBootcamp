namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            // Your Module 01 Challenge code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Challenge", "Coming Soon!");

            // create a transactionto lock the model
            Transaction t = new Transaction(doc);
            t.Start("I am doing something in Revit");


            int numberVariable = 250;
            int startingElevation = 0;
            int floorHeight = 15;

            // Loop through the number 1 to the number variable
            for (int i = 1; i <= numberVariable; i++)
            {
                // create a floor level
                Level newLevel = Level.Create(doc, startingElevation);
                newLevel.Name = "Level" + i;
                startingElevation = floorHeight + startingElevation;

                int divisible3 = i % 3;
                int divisible5 = i % 5;

                if (divisible3 == 0 && divisible5 == 0)
                {
                    //Get titleblock type
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfCategory(BuiltInCategory.OST_TitleBlocks);
                    collector1.WhereElementIsElementType();

                    // Create our sheet
                    ViewSheet newSheet = ViewSheet.Create(doc, collector1.FirstElementId());
                    newSheet.Name = "FIZZBUZZ_#";

                    // create a filtered element collector to get view family type
                    FilteredElementCollector collector2 = new FilteredElementCollector(doc);
                    collector2.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector2)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                        }
                    }

                    // create a floor plan view
                    ViewPlan newFloorPan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    newFloorPan.Name = "FIZZBUZZ_#" + i;

                    // Create a Viewport
                    // First create a point
                    XYZ insPoint = new XYZ();

                    Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPan.Id, insPoint);
                }
                else if (divisible3 == 0)
                {
                    // create a filtered element collector to get view family type
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                        }
                    }

                    // create a floor plan view
                    ViewPlan newFloorPan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    newFloorPan.Name = "FIZZ_#" + i;
                }
                else if (divisible5 == 0)
                {
                    // create a filtered element collector to get view family type
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType ceilingPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanVFT = curVFT;
                        }
                    }

                    // create a ceiling plan view
                    ViewPlan newceilingPan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
                    newceilingPan.Name = "BUZZ_#" + i;
                }
            }

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge01";
            string buttonTitle = "Module\r01";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module01,
                Properties.Resources.Module01,
                "Module 01 Challenge");



            return myButtonData.Data;
        }
    }

}

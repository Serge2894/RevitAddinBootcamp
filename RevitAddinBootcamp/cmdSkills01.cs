namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Lets create a variable

            // Create string variable
            string text1 = "this is my test";
            string text2 = "this is my test 2";

            // Combine the 2 strings
            string text3 = text1 + text2;
            string text4 = text1 + " " + text2;

            //Create number varibles
            int number1 = 10;
            double number2 = 20.5;


            // do sum math
            double number3 = number1 + number2;
            double number4 = number1 - number2;
            double number6 = number1 / number3;
            double number5 = number1 * number3;

            // convert meter to feet
            double meters = 4;
            double metersTofett = meters * 3.28084;

            // convert mm to feet
            double mm = 3500;
            double mmTofeet = mm / 304.6;
            double mmTofeet2 = (mm / 1000) * 3.28084;


            // find the remainder when dividing (ie. the modulo or mod)
            double remainder1 = 100 % 10; // euqals 0
            double remainder2 = 100 % 9; // equals 1

            // increment number by 1
            number5++;
            number3--;

            // check a value and perfrom single action if true
            if (number3 > 10)
            {
                // do something if true

            }
            if (number5 == 100)
            {
                // do something if true
            }
            else
            {
                // do something if false
            }

            // check multiple values and perfrom action if true and false
            if (number3 == 10)
            {

            }
            else if (number2 == 10)
            {

            }
            else
            {

            }

            // create compound conditional statement
            // check 2 or more things (&&)
            if (number1 == 10 && number3 > 10)
            {
                // do something
            }

            if (number3 ==10 || number1 == 20)
            {
                //do something
            }

            // create list
            List<string> list1 = new List<string>();

            // add items to the list
            list1.Add(text1);
            list1.Add(text2);
            list1.Add("this is my list");

            List<int> list2 = new List<int> {  1, 2, 3 };
            List<string> list3 = new List<string> { "1", "serge" };

            // loop through an item through a list with foreach loop
            foreach (string currentString in list1)
            {
                 // do something with current string
            }

            int letterCount = 0;
            foreach (string currentString in list1)
            {
                letterCount = letterCount + currentString.Length;
            }

            // loop through a range of numbers 
            for (int i =0; i <=10; i++)
            {
                // do something
            }

            int numberCount = 0;
            int counter = 100;
            for (int i = 0; i <=counter; i++)
            {
                // do something
                numberCount += i;
            }


            // Your Module 01 Skills code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Skills", "Got Here!");


            // create a transactionto lock the model
            Transaction t = new Transaction(doc);
            t.Start("I am doing something in Revit");

            // create a floor level
            Level newLevel = Level.Create(doc, 10);
            newLevel.Name = "My New Level";


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
            newFloorPan.Name = "Serge Floor Plan";

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
            newceilingPan.Name = "Serge Ceiling Plan";

            //Get titleblock type
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
            collector2.WhereElementIsElementType();

            // Create our sheet
            ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
            newSheet.Name = "Sheet1";
            newSheet.SheetNumber = "A101";

            // Create a Viewport
            // First create a point
            XYZ insPoint = new XYZ();
            XYZ insPoint2 = new XYZ(1, 0.5, 0);

            Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPan.Id, insPoint2);

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
    }

}

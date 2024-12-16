using RevitAddinBootcamp.Common;
using static RevitAddinBootcamp.MovingList;

namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge03 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            FurnitureInstance movingInstance1 = new FurnitureInstance("Classroom", "Desk", "Teacher", 1);
            FurnitureInstance movingInstance2 = new FurnitureInstance("Classroom", "Desk", "Student", 6);
            FurnitureInstance movingInstance3 = new FurnitureInstance("Classroom", "Chair-Desk", "Default", 7);
            FurnitureInstance movingInstance4 = new FurnitureInstance("Classroom", "Shelf", "Large", 1);

            List<FurnitureInstance> movingList = new List<FurnitureInstance> {movingInstance1, movingInstance2};

            foreach (FurnitureInstance Instance in movingList)
            {
                Room roomname = Utils.GetRoomByName(doc, Instance.RoomName);
                FamilySymbol curFamSymbol = Utils.GetFamilySymbolByName(doc, Instance.FamilyName, Instance.TypeName);
                curFamSymbol.Activate();

                for (int i = 0; i < Instance.Count; i++)
                {
                LocationPoint loc = roomname.Location as LocationPoint;
                FamilyInstance curFamInstance = doc.Create.NewFamilyInstance(loc.Point, curFamSymbol, StructuralType.NonStructural);
                }
            }
            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge03";
            string buttonTitle = "Module\r03";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module03,
                Properties.Resources.Module03,
                "Module 03 Challenge");

            return myButtonData.Data;
        }
    }

}

using RevitAddinBootcamp.Common;
using System.Linq;
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

            List<FurnitureInstance> movingList = new List<FurnitureInstance>();

            movingList.Add(new FurnitureInstance("Classroom", "Desk", "Teacher", 1));
            movingList.Add(new FurnitureInstance("Classroom", "Desk", "Student", 6));
            movingList.Add(new FurnitureInstance("Classroom", "Chair-Desk", "Default", 7));
            movingList.Add(new FurnitureInstance("Classroom", "Shelf", "Large", 1));
            movingList.Add(new FurnitureInstance("Office", "Desk", "Teacher", 1));
            movingList.Add(new FurnitureInstance("Office", "Chair-Executive", "Default", 1));
            movingList.Add(new FurnitureInstance("Office", "Shelf", "Small", 1));
            movingList.Add(new FurnitureInstance("Office", "Chair-Task", "Default", 1));
            movingList.Add(new FurnitureInstance("VR Lab", "Table-Rectangular", "Small", 1));
            movingList.Add(new FurnitureInstance("VR Lab", "Table-Rectangular", "Large", 8));
            movingList.Add(new FurnitureInstance("VR Lab", "Chair-Task", "Default", 9));
            movingList.Add(new FurnitureInstance("Computer Lab", "Desk", "Teacher", 1));
            movingList.Add(new FurnitureInstance("Computer Lab", "Desk", "Student", 10));
            movingList.Add(new FurnitureInstance("Computer Lab", "Chair-Desk", "Default", 11));
            movingList.Add(new FurnitureInstance("Computer Lab", "Shelf", "Large", 2));
            movingList.Add(new FurnitureInstance("Student Lounge", "Sofa", "Large", 2));
            movingList.Add(new FurnitureInstance("Student Lounge", "Table-Coffee", "Square", 2));
            movingList.Add(new FurnitureInstance("Teacher Lounge", "Sofa", "Small", 2));
            movingList.Add(new FurnitureInstance("Teacher Lounge", "Table-Coffee", "Large", 1));
            movingList.Add(new FurnitureInstance("Waiting", "Chair-Waiting", "Default", 2));
            movingList.Add(new FurnitureInstance("Waiting", "Table-Coffee", "Large", 1));

            int totalcounter = 0;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Move in Furniture");
                foreach (FurnitureInstance Instance in movingList)
                {
                    List<Room> roomList = Utils.GetRoomsByName(doc, Instance.RoomName);
                    foreach (Room room in roomList)
                    {
                        int roomCounter = 0;

                        FamilySymbol curFamSymbol = Utils.GetFamilySymbolByName(doc, Instance.FamilyName, Instance.TypeName);
                        curFamSymbol.Activate();
                        for (int i = 0; i < Instance.Count; i++)
                        {
                            LocationPoint loc = room.Location as LocationPoint;
                            FamilyInstance curFamInstance = doc.Create.NewFamilyInstance(loc.Point, curFamSymbol, StructuralType.NonStructural);
                            totalcounter++;
                            roomCounter++;
                        }
                        //4. update furniture count for room
                        Parameter roomCount = room.LookupParameter("Furniture Count");

                        if (roomCount != null)
                        {
                            roomCount.Set(roomCounter);
                        }
                    }
                }
                t.Commit();

            }

            //5. alert user
            TaskDialog.Show("Complete", $"You moved {totalcounter} pieces of furniture into the building. Great work!");

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

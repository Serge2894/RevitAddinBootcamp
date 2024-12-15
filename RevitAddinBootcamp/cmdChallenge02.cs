namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge02 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            // Select mulitple elements
            List<Element> pickList = uidoc.Selection.PickElementsByRectangle("Select Elements").ToList();

            TaskDialog.Show("Selected elements", $"I selected {pickList.Count} elements.");

            // 3. Get level and various types
            Level currentLevel = GetLevelByName(doc, "Level 1");

            // 4. Get types
            WallType wt1 = GetWallTypeByName(doc, "Storefront");
            WallType wt2 = GetWallTypeByName(doc, "Generic - 8\"");

            MEPSystemType ductSystemType = GetMEPSystemTypeByName(doc, "Supply Air");
            DuctType ductType = GetDuctTypeByName(doc, "Default");

            MEPSystemType pipeSystemType = GetMEPSystemTypeByName(doc, "Domestic Hot Water");
            PipeType pipeType = GetPipeTypeByName(doc, "Default");

            List<ElementId> linesToHide = new List<ElementId>();

            // Filter selected elements for model curves
            List<CurveElement> modelCurves = new List<CurveElement>();
            foreach (Element element in pickList)
            {
                if (element is CurveElement)
                {
                    //CurveElement curveElem = element as CurveElement;
                    CurveElement curveElement = (CurveElement)element;

                    if (curveElement.CurveElementType == CurveElementType.ModelCurve)
                    {
                        modelCurves.Add(curveElement);
                    }
                }
            }

            // Loop through model curve
            foreach (CurveElement currentCurve in modelCurves)
            {
                // Get Curve
                Curve curve = currentCurve.GeometryCurve;

                // Get Graphicstyle
                GraphicsStyle curStyle = currentCurve.LineStyle as GraphicsStyle;

                Debug.Print(curStyle.Name);

                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Create Element based on curve line t");

                    switch (curStyle.Name)
                    {
                        case "A-GLAZ":
                            // create wall
                            Wall newWall1 = Wall.Create(doc, curve, wt1.Id, currentLevel.Id, 20, 0, false, false);

                            break;
                        case "A-WALL":
                            // create wall
                            Wall newWall2 = Wall.Create(doc, curve, wt1.Id, currentLevel.Id, 20, 0, false, false);
                            break;
                        case "M-DUCT":
                            // create duct
                            Duct newDuct = Duct.Create(doc, ductSystemType.Id, ductType.Id,
                                currentLevel.Id, curve.GetEndPoint(0), curve.GetEndPoint(1));
                            break;
                        case "P-PIPE":
                            // create pipe
                            Pipe newPipe = Pipe.Create(doc, pipeSystemType.Id, pipeType.Id,
                                currentLevel.Id, curve.GetEndPoint(0), curve.GetEndPoint(1));
                            break;
                    }
                    t.Commit();
                }
            }
            return Result.Succeeded;
        }

        private PipeType GetPipeTypeByName(Document doc, string typeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(PipeType));

            foreach (PipeType curType in collector)
            {
                if (curType.Name == typeName)
                    return curType;
            }

            return null;
        }

        private DuctType GetDuctTypeByName(Document doc, string typeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(DuctType));

            foreach (DuctType curType in collector)
            {
                if (curType.Name == typeName)
                    return curType;
            }

            return null;
        }

        private MEPSystemType GetMEPSystemTypeByName(Document doc, string typeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(MEPSystemType));

            foreach (MEPSystemType curType in collector)
            {
                if (curType.Name == typeName)
                    return curType;
            }

            return null;
        }

        private WallType GetWallTypeByName(Document doc, string typeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(WallType));

            foreach (WallType curType in collector)
            {
                if (curType.Name == typeName)
                    return curType;
            }

            return null;
        }

        private Level GetLevelByName(Document doc, string levelName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(Level));

            foreach (Level curLevel in collector)
            {
                if (curLevel.Name == levelName)
                    return curLevel;
            }

            return null;
        }


        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge02";
            string buttonTitle = "Module\r02";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module02,
                Properties.Resources.Module02,
                "Module 02 Challenge");

            return myButtonData.Data;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois
{


    public static class GraphColor
    {
        public static string[] BorderColor = { "#1565C0", "#FF80AB", "#42A5F5", "#D81B60", "#6200EA", "#039BE5", "#0097A7", "#009688", "#E0F7FA", "#81C784", "#8BC34A", "#F0F4C3", "#81C784", "#00C853", "#FFF176", "#FFB74D", "#FF5722", "#8D6E63", "#212121", "#90A4AE", "#FFEE58", "#FFF8E1", "#40C4FF", "#00838F", "#00695C", "#4FC3F7", "#7C4DFF", "#5C6BC0", "#EDE7F6", "#D50000", "#FF80AB", "#7B1FA2", "#FFCDD2", "#9FA8DA", "#4DB6AC", "#80D8FF", "#689F38", "#FFF176", "#FF9800", "#FF7043", "#5D4037", "#FB8C00", "#81C784", "#DCE775", "#FFCA28", "#AD1457", "#9C27B0", "#3949AB", "#64B5F6", "#4CAF50"};
    }

    public static class Documents
    {
        public static string RemoteImageUrl = "http://localhost:24116/Documents/Image/";
        public static string RemoteFileUrl = "http://localhost:24116/Documents/File/";

        //        public static string RemoteImageUrl = "http://localhost:24123/Documents/Image/";
        //        public static string RemoteFileUrl = "http://localhost:24123/Documents/File/";
    }
    public enum ReportType
    {
        Pdf = 1,
        Word = 2,
        Excel = 3
    }

    public enum PageOrientation
    {
        Potrait = 1,
        Landscape = 2
        
    }

    public enum AddressType
    {
        Permanent = 1,
        Present = 2
    }

    public enum FileType
    {
        Picture = 1,
        Signature = 2
    }


    public enum ColorType
    {

        Skin = 1,
        Hair = 2,
        Eyes = 3

    }


    public enum EarlyStatus
    {
        Early = 1,
        Later = 2
    }

    public enum BoardType
    {
        Board = 1,
        University = 2
    }
    public enum RetiredStatus
    {
        Reserve = 1,
        Emergency = 2,
        None = 4,
    }

    public enum RStatus
    {
        Retired = 1,
        Emergency = 2,
        Reserve = 3
    }

    //public enum LprStatus
    //{
    //    Retirement = 1,
    //    Terminated = 2,
    //    LPR = 3
    //}

    public enum OfficerCurrentStatus
    {
        Active = 1,
        Retired = 2,
        Run = 3,
        Missing = 4,
        Dead = 5,
        LPR = 6,
        Terminated = 7,
        Back_TO_Unit = 8,
        Return_From_Run_Missing = 9,
        Rejoin = 10

    }


    //public enum OfficerCurrentStatus
    //{
    //    Retired = 1,
    //    LPR = 2,
    //    Missing = 3,
    //    Run = 4,
    //    Dead = 7,
    //    Back_to_Unit = 9,
    //    Return_From_Run_Missing = 9,
    //    Active = 10

    //}

    public enum CountryType
    {
        Local = 1,
        Foreign = 2
    }

    public enum ResultStatus
    {
        Good = 1,
        Poor = 2
    }

  

    public enum SiblingType
    {
        Brother=1,
        Sister=2
    }

    public enum RelationType
    {
        First=1,
        Second=2,
        Third=3
    }

    public enum ParentTypes
    {
        Father=1,
        Mother=2,
        StepFather=3,
        StepMother=4
    }

    public enum CurrentStatus
    {
        Alive=1,
        Divorce = 2,
        Dead =3
        
    }

    public enum HeirKinType
    {
        Next_Of_Kin=1,
        Heir=2,
    }


    public enum ShipType
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public enum Objective
    {
        Training = 1,
        Operational = 2,
        Both = 3,
        None = 4
    }

    public enum PhotoType
    {
        Image = 1,
        Signature = 2
    }

	public enum MedalType
	{
		PreCommission=1,
		PostCommission=2
	}

    public enum CommendationType
    {
        Commendation=1,
        Appreciation=2
    }

    public enum AchievementComType
    {
        Commendation=1,
        Appreciation=2,
        Notable_Achievement=3
    }

    public enum GivenByType
    {
        BN = 1,
        Others = 2
    }

    public enum MedalAwardType
    {
        Award = 1,
        Medal = 2,
        Publication = 3

    }

    public enum ObservationIntelligentType
    {
        Observation = 1,
        Intelligent_Report= 2

    }

    public enum PunishmentAccidentType
    {
     Punishment=1,
     Accident=2

    }

    public enum AccidentType
    {
        Major=1,
        Minor=2
    }
	


	public enum NominationType
    {
        Course =1,
		Mission =2,
        Foreign_Visit=3,
        Other= 4
    }

	public enum DurationStatus
	{
		Month =1,
		Day = 2
	}


    public enum TransferMode
    {
       Permanent =1,
       Temporary = 2
    }


    public enum TransferType
    {
        Inside=1,
        Outside =2,
        CostGuard=3,
    }

    public enum TransferFor
    {
        Office = 1,
        Course = 2,
        Mission = 3
    }

    public enum TemporaryTransferType
    {
        TY_Duty=1,
        TY_Attachment=2
//        TY_Appointment=3
    }


	public enum NominationScheduleType
	{
		Mission = 1,
		Foreign_Visit = 2,
        Other = 3
	}
    public enum ChildrenType
    {
        Son = 1,
        Daughter = 2
    }

    public enum ImageEnum
    {
        Picture = 1,
        Signature = 2
      
    }

    public enum FeatureType
    {
        Feature = 1,
        Report = 2,
        CurrentStatus = 3,
    }
}

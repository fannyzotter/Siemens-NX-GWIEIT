namespace FirstNxOpenTestProject
{
    public class Program
    {
        public static void Main()
        {
            //NXOpen.Session.GetSession().ListingWindow.Open();
            //NXOpen.Session.GetSession().ListingWindow.WriteLine("Hello World!");

            ShowCAMOperations dialog = new ShowCAMOperations();
            dialog.Launch();
        }
    }
}
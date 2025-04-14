namespace FirstNxOpenTestProject
{
    public class Program
    {
        public class PMICAMDialog
        {
            public static void Main(string[] args)
            {
                // Create a new instance of the pmicam1 class
                pmicam1 dialog = new pmicam1();
                // Call the Show method to display the PMI
                dialog.Launch();
            }
        }
    }
}
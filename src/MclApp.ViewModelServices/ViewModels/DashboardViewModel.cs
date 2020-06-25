namespace MclApp.ViewModelServices.ViewModels
{
    public class DashboardViewModel
    {
        public string CurrentCyberScore { get; set; }
        public string CurrentCyberValue { get; set; }
        
        public string TargetScore { get; set; }
        public string ImprovementTasks { get; set; }

        public string UserTasks { get; set; }
        public string Actions { get; set; }

        public string GoalScoreTracker { get; set; }

        public string CurrentCyberScoreForChart { get; set; }
        //public List<CyberVulnerabilityViewModel> CyberVulnerabilityViewModel { get; set; }

    }
}

namespace QuestionManager.BLL.Models.Requests
{
    public class AddRequest
    {
        public string Question { get; set; }
        public string Answear { get; set; }
        public int Complexity { get; set; }
        public string SecondOption { get; set; }
        public string ThirdOption { get; set; }
    }
}

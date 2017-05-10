namespace VisualStudioAutomation
{
    using TestStack.White.UIItems.Finders;
    using TestStack.White.UIItems.WindowItems;

    public class NewProjectDialog
    {
        private readonly Window window;

        public NewProjectDialog(Window window)
        {
            this.window = window;
        }

        public void SelectType(string type)
        {
            var item = window.Get<TestStack.White.UIItems.Label>(SearchCriteria.ByText(type));

            item.Click();

        }

        public void SetName(string projectName)
        {
            var textBox = window.Get<TestStack.White.UIItems.TextBox>(SearchCriteria.ByAutomationId("txt_Name"));

            textBox.Text = projectName;
        }

        public void ClickOk()
        {
            var button = window.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByText("OK"));

            button.Click();
        }
    }
}
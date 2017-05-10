namespace VisualStudioAutomation
{
    using System;
    using System.Windows.Automation;
    using TestStack.White.UIItems;
    using TestStack.White.UIItems.Finders;
    using TestStack.White.UIItems.TreeItems;
    using TestStack.White.UIItems.WindowItems;
    using TestStack.White.UIItems.WPFUIItems;

    public class MainWindow
    {
        private readonly Window window;

        public MainWindow(Window window)
        {
            this.window = window;
        }

        public NewProjectDialog ClickNewProject()
        {
            IUIItem file = window.Get(SearchCriteria.ByText("File"));
            file.Click();

            IUIItem newItem = window.Get(SearchCriteria.ByText("New"));

            newItem.Click();

            IUIItem projectItem = window.Get(SearchCriteria.ByText("Project..."));
            projectItem.Click();


            // Just take the first since it will (should!) be the only one
            var dialog = new NewProjectDialog(window.ModalWindows()[0]);

            return dialog;
        }

        public void AddReference(string referenceName)
        {
            var solutionExplorerTab = window.Get(SearchCriteria.ByText("Solution Explorer"));

            // Focus the tab
            solutionExplorerTab.Click();

            var solutionExplorerTree = window.Get<Tree>(SearchCriteria.ByAutomationId("SolutionExplorer"));
            
            var node = FindAutomationElement(solutionExplorerTree.AutomationElement, "References");

            var nodeItem = new UIItem(node, window);

            // Focus
            nodeItem.Click();

            // Click
            nodeItem.RightClick();

            window.Popup.Item("Add Reference...").Click();

            var dialog = window.ModalWindows()[0];

            var listItem = dialog.Get(SearchCriteria.ByText(referenceName));

            listItem.Click();
            
            var checkBox = listItem.Get<CheckBox>(SearchCriteria.ByControlType(ControlType.CheckBox));

            checkBox.Checked = true;

            dialog.Get<Button>(SearchCriteria.ByText("OK")).Click();
        }

        private static AutomationElement FindAutomationElement(AutomationElement element, string text)
        {
            if (element == null)
            {
                return null;
            }

            AutomationElement elementNode = TreeWalker.ControlViewWalker.GetFirstChild(element);

            while (elementNode != null)
            {
                if (elementNode.Current.Name.Equals(text, StringComparison.InvariantCultureIgnoreCase))
                {
                    return elementNode;
                }

                // Iterate to next element.
                var value = FindAutomationElement(elementNode, text);

                if (value != null)
                {
                    return value;
                }

                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode);
            }

            return null;
        }
    }
}
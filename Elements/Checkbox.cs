using OpenQA.Selenium;

namespace TestRail.Elements
{
    public class Checkbox
    {
        private readonly UiElement _uiElement;

        public Checkbox(IWebDriver driver, By locator)
        {
            _uiElement = new UiElement(driver, locator);
        }

        public void Select()
        {
            if (!_uiElement.Selected)
            {
                _uiElement.Click();
            }
        }

        public void Deselect()
        {
            if (_uiElement.Selected)
            {
                _uiElement.Click();
            }
        }
        public bool Displayed => _uiElement.Displayed;
        public bool Enabled => _uiElement.Enabled;
        public string Text => _uiElement.Text;
        public bool Selected => _uiElement.Selected;

        public string GetAttribute(string attributeName) => _uiElement.GetAttribute(attributeName);
        public bool IsChecked()
        {
            if (_uiElement.GetAttribute("checked") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

using OpenQA.Selenium;

namespace TestRail.Elements
{
    public class Field
    {
        private readonly UiElement _uiElement;

        public Field(IWebDriver driver, By locator)
        {
            _uiElement = new UiElement(driver, locator);
        }
        public void SendKeys(string text) => _uiElement.SendKeys(text);
        public void Click() => _uiElement.Click();
        public bool Displayed => _uiElement.Displayed;
        public bool Enabled => _uiElement.Enabled;
        public string Text => _uiElement.Text;
        public string GetAttribute(string attributeName) => _uiElement.GetAttribute(attributeName);
    }
}

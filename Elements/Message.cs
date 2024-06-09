using OpenQA.Selenium;

namespace TestRail.Elements
{
    public class Message
    {
        private readonly UiElement _uiElement;
        public Message(IWebDriver driver, By locator)
        {
            _uiElement = new UiElement(driver, locator);
        }
        public bool Displayed => _uiElement.Displayed;
        public string Text => _uiElement.Text.Trim();
        public string GetAttribute(string attributeName) => _uiElement.GetAttribute(attributeName);
    }
}

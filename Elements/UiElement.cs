using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Drawing;

namespace TestRail.Elements
{
    public class UiElement : IWebElement
    {
        private readonly IWebDriver _driver;
        private readonly IWebElement _element;
        private readonly Actions _actions;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public string TagName => _element.TagName;

        public string Text
        {
            get
            {
                if (_element.Text == null || _element.Text == "")
                {
                    if (GetAttribute("value") == null)
                    {
                        return GetAttribute("innertext");
                    }
                    else
                    {
                        return GetAttribute("value");
                    }
                }
                else
                {
                    return _element.Text;
                }
            }
        }

        public bool Enabled => _element.Enabled;

        public bool Selected => _element.Selected;

        public Point Location => _element.Location;

        public Size Size => _element.Size;

        public bool Displayed => _element.Displayed;

        private UiElement(IWebDriver driver)
        {
            _driver = driver;
            _actions = new Actions(_driver);
        }
        public UiElement(IWebDriver driver, By locator) : this(driver)
        {
            _element = _driver.FindElement(locator);
        }
        public UiElement(IWebDriver driver, IWebElement element) : this(driver)
        {
            _element = element;
        }

        public void Clear() => _element.Clear();

        public void Click()
        {
            try
            {
                _element.Click();
            }
            catch (ElementNotInteractableException)
            {
                try
                {
                    _actions
                   .MoveToElement(_element)
                   .Click()
                   .Build()
                   .Perform();
                    _logger.Warn("Сlicked using actions");
                }
                catch (ElementNotInteractableException)
                {
                    MoveToElement();
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].сlick();", _element);
                    _logger.Warn("Сlicked using Java Script Executor");
                }
            }
        }

        public IWebElement FindElement(By by) => _element.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By by) => _element.FindElements(by);

        public string GetAttribute(string attributeName) => _element.GetAttribute(attributeName);

        public string GetCssValue(string propertyName) => _element.GetCssValue(propertyName);

        public string GetDomAttribute(string attributeName) => _element.GetDomAttribute(attributeName);

        public string GetDomProperty(string propertyName) => _element.GetDomProperty(propertyName);

        public ISearchContext GetShadowRoot() => _element.GetShadowRoot();

        public void SendKeys(string text)
        {
            _element.SendKeys(text);
            try
            {
                if (_element.GetAttribute("value") != null && _element.GetAttribute("value") != text)
                {
                    _element.Clear();        
                        _actions
                        .MoveToElement(_element)
                        .Click()
                        .SendKeys(text)
                        .Build()
                        .Perform();
                    _logger.Warn("Sent keys using actions");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Actions 'Send keys' cannot be performed! " + ex);
                throw;
            }
        }

        public void Submit()
        {
            try
            {
                _element.Submit();
            }
            catch
            {
                _element.SendKeys(Keys.Enter);
            }
        }

        public void MoveToElement()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", _element);
        }

        public void Hover()
        {
            _actions
                .MoveToElement(_element)
                .Build()
                .Perform();
        }
    }
}

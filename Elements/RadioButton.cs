using NLog;
using OpenQA.Selenium;

namespace TestRail.Elements
{
    public class RadioButton
    {
        private readonly List<UiElement> _uiElements;
        private readonly List<int> _values;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public RadioButton(IWebDriver driver, By locator)
        {
            _uiElements = new List<UiElement>();
            _values = new List<int>();
            foreach (IWebElement element in driver.FindElements(locator))
            {
                UiElement uiElement = new UiElement(driver, element);
                _uiElements.Add(uiElement);
                if (int.TryParse(element.GetAttribute("value"), out int result))
                {
                    _values.Add(result);
                }
            }
        }

        /// <summary>
        /// Indexing starts from 0
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SelectByIndex(int index)
        {
            if (index < _uiElements.Count && index >= 0)
            {
                _uiElements[index].Click();
            }
            else
            {
                _logger.Error($"Couldn't find radio button with index: {index}!");
                throw new ArgumentOutOfRangeException($"Couldn't find radio button with index: {index}!");
            }
        }

        public void SelectByValue(int? value)
        {
            if (value.HasValue)
            {
                int index = _values.IndexOf(value.Value);
                if (index == -1)
                {
                    _logger.Error($"Couldn't find radio button with value: {value}!");
                    throw new ArgumentOutOfRangeException($"Couldn't find radio button with value: {value}!");
                }
                else
                {
                    _uiElements[index].Click();
                }
            }
            else
            {
                _logger.Error("The value of radio button is null!");
                throw new ArgumentNullException("The value of radio button is null!");
            }
        }
    }
}

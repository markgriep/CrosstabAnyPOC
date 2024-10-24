using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrosstabAnyPOC.DataAccess.Models;

namespace CrosstabAnyPOC.Utilities
{
    internal class MockTestSettingsHelper
    {

        public DrugTestSettings RandomTestSettings { get; set; } = new DrugTestSettings();
        public DrugTestSettings ManuallySetTestSettings { get; set; } = new DrugTestSettings();


        public MockTestSettingsHelper()
        {
            PopulateRandomSettings();
            ManuallySetTestSettings = RandomTestSettings;   // set the manual one so there's some kind of value in it.
        }


        /// <summary>
        /// These setting you can mostly set manually
        /// ALSO does a random setting to have it available
        /// </summary>
        /// <param name="testTypeEnum"></param>
        /// <param name="name"></param>
        /// <param name="tgEnum"></param>
        /// <param name="tcEnum"></param>
        /// <param name="tssmEnum"></param>
        /// <param name="drugTestPercentage"></param>
        /// <param name="alcoholTestPercentage"></param>
        public MockTestSettingsHelper( 
            TestType testTypeEnum = TestType.Both,
            string name = "MarkG", 
            TestingGroup tgEnum = TestingGroup.T, 
            TestCategory tcEnum = TestCategory.Random,
            TestSubjectSelectionMethod tssmEnum = TestSubjectSelectionMethod.Automatic,
            decimal drugTestPercentage = 10M,
            decimal alcoholTestPercentage = 2M
            )
        {
            ManuallySetTestSettings.TestNumber = new Random().Next(114, 999);
            ManuallySetTestSettings.TestOperatorName = name;
            ManuallySetTestSettings.RequestDateTime = DateTime.Now;
            
            ManuallySetTestSettings.TestType = testTypeEnum;
            ManuallySetTestSettings.TestingGroup = tgEnum;
            ManuallySetTestSettings.TestCategory = tcEnum;
            ManuallySetTestSettings.TestSubjectSelectionMethod = tssmEnum;
            
            ManuallySetTestSettings.PercentageOfEmployeesToDrugTest = drugTestPercentage;
            ManuallySetTestSettings.PercentageOfEmployeesToAlcoholTest = alcoholTestPercentage;

            PopulateRandomSettings();
        }


        // populate RandomSettings with some ficticious values
        private void PopulateRandomSettings()
        {
            RandomTestSettings.TestNumber = new Random().Next(114, 999);
            RandomTestSettings.TestOperatorName = NameUtility.GenerateRandomFullName();
            RandomTestSettings.RequestDateTime = DateTime.Now.AddDays(new Random().Next(-1, -2999));

            RandomTestSettings.TestType = TestType.Drug;
            RandomTestSettings.TestingGroup = TestingGroup.T;
            RandomTestSettings.TestCategory = TestCategory.Random;
            RandomTestSettings.TestSubjectSelectionMethod = TestSubjectSelectionMethod.Automatic;

            RandomTestSettings.PercentageOfEmployeesToDrugTest = new Random().Next(20, 54);
            RandomTestSettings.PercentageOfEmployeesToAlcoholTest = new Random().Next(2, 12);   // this number is always lower drug test percentage

        
        }
    }
}

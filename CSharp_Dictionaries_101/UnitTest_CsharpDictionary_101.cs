using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharp_Dictionaries_101
{
    public class UnitTest_CsharpDictionary_101
    {

        [Fact]
        public void Test_Dictionary_Different_Ways_To_Create_A_New_Instance()
        {
            IDictionary<string, string> countries = new Dictionary<string, string>();
            
            Assert.NotNull(countries);

            Dictionary<string, string> asianCountries = new Dictionary<string, string>();

            Assert.NotNull(asianCountries);
        }

        [Fact]
        public void Test_Dictionary_Different_Ways_To_Create_A_New_Instance_And_Initialize()
        {
            Dictionary<string, string> asianCountries = new Dictionary<string, string>();

            asianCountries.Add("PH", "Philippines");

            Dictionary<string, string> asianCountries2 = new Dictionary<string, string>
            {
                { "PH", "Philippines" }
            };

            Dictionary<string, string> asianCountries3 = new Dictionary<string, string>
            {
                ["PH"] = "Philippines"
            };

            Assert.NotNull(asianCountries);
            Assert.NotNull(asianCountries2);
            Assert.NotNull(asianCountries3);
        }

        [Fact]
        public void Test_Dictionary_Cant_Have_Same_Key()
        {
            Dictionary<string, string> sampleDictionary = new Dictionary<string, string>();

            sampleDictionary.Add("MJ", "Michael Jordan");

            //throws an exception when adding an existing key
            Assert.Throws<ArgumentException>(() => sampleDictionary.Add("MJ", "Magic Johnson"));

            //TryMethod tries to add a new key but fails because MJ already exists
            Assert.False(sampleDictionary.TryAdd("MJ", "Magic Johnson"));
        }

        [Fact]
        public void Test_Dictionary_Cant_Have_A_Null_Key()
        {
            Dictionary<string, string> nullDictionary = new Dictionary<string, string> { };

            nullDictionary.Add("USA", null); //passed when adding a null value

            //throws an exception when adding a null as key
            Assert.Throws<ArgumentNullException>(() => nullDictionary.Add(null, null));
            Assert.Throws<ArgumentNullException>(() => nullDictionary.TryAdd(null, null));
        }


        [Fact]
        public void Test_Dictionary_To_Add()
        {
            var basketBallPlayers = new Dictionary<string, string>();

            basketBallPlayers.Add("MJ", "Michael Jordan");

            bool successInAdding_MJ_Key = basketBallPlayers.TryAdd("MJ", "Magic Johnson");

            if (!successInAdding_MJ_Key)
            {
                basketBallPlayers.TryAdd("KD", "Kevin Durant");
            }

            Assert.Collection(basketBallPlayers,
                                item => Assert.Contains("MJ", item.Key),
                                item => Assert.Contains("KD", item.Key));
        }

        [Fact]
        public void Test_Dictionary_GetValues()
        {
            var basketBallPlayers = new Dictionary<string, string>
            {
                ["MJ"] = "Michael Jordan",
                ["KD"] = "Kevin Durant"
            };

            //gets the value of the keys: MJ and KD
            Assert.True(!string.IsNullOrEmpty(basketBallPlayers["MJ"]));
            Assert.True(!string.IsNullOrEmpty(basketBallPlayers["KD"]));

            //try to get a key that doesn't exists
            Assert.Throws<KeyNotFoundException>(() => basketBallPlayers["LJ"]);
        }

        [Fact]
        public void Test_Dictionary_GetValues_Via_TryGetValue_Method()
        {
            
            var basketBallPlayers = new Dictionary<string, string>
            {
                ["MJ"] = "Michael Jordan",
                ["KD"] = "Kevin Durant"
            };

            string result = string.Empty;

            //tries to get the value of LJ.
            //if doesn't exists the string result will be equivalent to null.
            basketBallPlayers.TryGetValue("LJ", out result); 

            //once equivalent to null. Let's add LJ
            if (string.IsNullOrEmpty(result))
            {
                basketBallPlayers.Add("LJ", "Larry Johnson");
            }

            //let's try againg if LJ exits and get the value
            basketBallPlayers.TryGetValue("LJ", out result);

            //now it does exists
            Assert.True(!string.IsNullOrEmpty(result));
        }


        [Fact]
        public void Test_Dictionary_GetValues_And_Update()
        {
            //initialize new basketaball players
            var basketBallPlayers = new Dictionary<string, string>
            {
                ["MJ"] = "Michael Jordan",
                ["KD"] = "Kevin Durant",
                ["KJ"] = "Kill Joy"
            };

            //let us check if the dictionary collection contains a KJ key
            if (basketBallPlayers.ContainsKey("KJ")) 
            {
                //update the value of KJ key
                basketBallPlayers["KJ"] = "Kevin Johnson";

                Assert.True(basketBallPlayers["KJ"] == "Kevin Johnson");
            }

            //let us check if the dictionary collection contains a Michael Jordan as value
            if (basketBallPlayers.ContainsValue("Michael Jordan")) 
            {
                var result = basketBallPlayers.FirstOrDefault(value => value.Value == "Michael Jordan");
                
                //update the value from Michael Jordan to Magic Johnson
                basketBallPlayers[result.Key] = "Magic Johnson";

                Assert.True(basketBallPlayers[result.Key] != "Michael Jordan");
                Assert.True(basketBallPlayers[result.Key] == "Magic Johnson");
            }
        }

        [Fact]
        public void Test_Dictionary_Remove_Item_WithIn_The_Collection()
        {
            //initialize new basketaball players
            var basketBallPlayers = new Dictionary<string, string>
            {
                ["MJ"] = "Michael Jordan",
                ["KD"] = "Kevin Durant",
                ["KJ"] = "Kill Joy"
            };

            Assert.Throws<ArgumentNullException>(() => basketBallPlayers.Remove(null));

            //let us remove the key MJ
            var removedAtFirstAttempt = basketBallPlayers.Remove("MJ");

            Assert.True(removedAtFirstAttempt);

            //let us try to remove MJ a non existing key
            var removedAtSecondAttempt = basketBallPlayers.Remove("MJ");

            Assert.False(removedAtSecondAttempt);
        }
    }
}


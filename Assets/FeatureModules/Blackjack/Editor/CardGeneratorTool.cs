using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Blackjack.Editor
{
    public class CardGeneratorTool : EditorWindow
    {
        private string _spriteFolderPath = "Assets/FeatureModules/Blackjack/Assets/Cards";
        private string _outputFolderPath = "Assets/FeatureModules/Blackjack/Cards";

        private Sprite _cardBackSprite;

        [MenuItem("Tools/Card Generator Tool")]
        public static void ShowWindow() => 
            GetWindow<CardGeneratorTool>("Card Generator Tool");

        private void OnGUI()
        {
            GUILayout.Label("Card ScriptableObject Generator", EditorStyles.boldLabel);

            _spriteFolderPath = EditorGUILayout.TextField("Sprite Folder Path", _spriteFolderPath);
            _outputFolderPath = EditorGUILayout.TextField("Output SO Folder", _outputFolderPath);

            _cardBackSprite = (Sprite)EditorGUILayout.ObjectField("Card Back Sprite", _cardBackSprite, typeof(Sprite), false);

            if (!GUILayout.Button("Generate Card ScriptableObjects")) return;
            
            if (_cardBackSprite == null)
            {
                Debug.LogError("Please assign the Card Back Sprite first.");
                return;
            }

            GenerateCardDataAssets();
        }

        private void GenerateCardDataAssets()
        {
            if (!Directory.Exists(_outputFolderPath))
                Directory.CreateDirectory(_outputFolderPath);

            var spriteGuids = AssetDatabase.FindAssets("t:Sprite", new[] { _spriteFolderPath });

            foreach (var guid in spriteGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

                var fileName = Path.GetFileNameWithoutExtension(path);

                if (fileName.ToLower() == "cardback")
                    continue;

                if (!TryParseCardName(fileName, out CardRank rank, out CardSuit suit))
                {
                    Debug.LogWarning($"Skipped sprite with unrecognized name: {fileName}");
                    continue;
                }

                var cardData = CreateInstance<Card>();
                cardData.rank = rank;
                cardData.suit = suit;
                cardData.cardSprite = sprite;
                cardData.cardBackSprite = _cardBackSprite;

                var assetPath = $"{_outputFolderPath}/{fileName}.asset";
                AssetDatabase.CreateAsset(cardData, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("CardData ScriptableObjects generated!");
        }

        private bool TryParseCardName(string name, out CardRank rank, out CardSuit suit)
        {
            rank = default;
            suit = default;

            if (!name.StartsWith("card", StringComparison.OrdinalIgnoreCase))
                return false;

            var trimmed = name.Substring(4); // remove "card" prefix

            foreach (CardSuit s in Enum.GetValues(typeof(CardSuit)))
            {
                var suitName = s.ToString();
                if (!trimmed.StartsWith(suitName, StringComparison.OrdinalIgnoreCase)) continue;
                
                var shortRank = trimmed.Substring(suitName.Length);
                if (!TryParseShortRank(shortRank, out rank)) continue;
                
                suit = s;
                return true;
            }

            return false;
        }

        private bool TryParseShortRank(string shortRank, out CardRank rank)
        {
            return shortRank switch
            {
                "A" => Set(CardRank.Ace, out rank),
                "K" => Set(CardRank.King, out rank),
                "Q" => Set(CardRank.Queen, out rank),
                "J" => Set(CardRank.Jack, out rank),
                "10" => Set(CardRank.Ten, out rank),
                "9" => Set(CardRank.Nine, out rank),
                "8" => Set(CardRank.Eight, out rank),
                "7" => Set(CardRank.Seven, out rank),
                "6" => Set(CardRank.Six, out rank),
                "5" => Set(CardRank.Five, out rank),
                "4" => Set(CardRank.Four, out rank),
                "3" => Set(CardRank.Three, out rank),
                "2" => Set(CardRank.Two, out rank),
                _ => Set(CardRank.Two, out rank, false) // fallback
            };

            static bool Set(CardRank r, out CardRank result, bool success = true)
            {
                result = r;
                return success;
            }
        }
    }
}

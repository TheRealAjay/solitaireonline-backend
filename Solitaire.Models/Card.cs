using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Solitaire.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverterEx<CardType>))]
        public CardType Type { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverterEx<Value>))]
        public Value Value { get; set; }
        public string Position { get; set; } = null!;
        public bool Flipped { get; set; }

        public virtual int SolitaireSessionId { get; set; }
        public virtual SolitaireSession SolitaireSession { get; set; }
    }

    public enum CardType
    {
        Heart,   // Herz
        Diamond, // Raute
        Spade,   // Pic
        Clover   // Kreuz
    }

    // Wert
    public enum Value
    {
        [EnumMember(Value = "1")]
        Rank_A = 1,
        [EnumMember(Value = "2")]
        Rank_2,
        [EnumMember(Value = "3")]
        Rank_3,
        [EnumMember(Value = "4")]
        Rank_4,
        [EnumMember(Value = "5")]
        Rank_5,
        [EnumMember(Value = "6")]
        Rank_6,
        [EnumMember(Value = "7")]
        Rank_7,
        [EnumMember(Value = "8")]
        Rank_8,
        [EnumMember(Value = "9")]
        Rank_9,
        [EnumMember(Value = "10")]
        Rank_10,
        [EnumMember(Value = "11")]
        Rank_J,
        [EnumMember(Value = "12")]
        Rank_Q,
        [EnumMember(Value = "13")]
        Rank_K
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCompare.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [JsonPropertyName("make")]
        [Required]
        public string? Make { get; set; }

        [JsonPropertyName("model")]
        [Required]
        public string? Model { get; set; }

        [JsonPropertyName("year")]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [JsonPropertyName("price_mxn")]
        public string PriceMxn { get; set; }

        [JsonPropertyName("engine_specs")]
        public EngineSpecs? EngineSpecs { get; set; }

        [JsonPropertyName("fuel_efficiency_kml")]
        public FuelEfficiency? FuelEfficiencyKml { get; set; }

        [JsonPropertyName("safety_features")]
        public List<string>? SafetyFeatures { get; set; }
    }

    public class EngineSpecs
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("horsepower_hp")]
        public object? HorsepowerHp { get; set; }

        [JsonPropertyName("torque_lbft")]
        public object? TorqueLbft { get; set; }

        [JsonPropertyName("torque_nm")]
        public object? TorqueNm { get; set; }
    }

    public class FuelEfficiency
    {
        [JsonPropertyName("combined")]
        public object? Combined { get; set; }

        [JsonPropertyName("city")]
        public object? City { get; set; }

        [JsonPropertyName("highway")]
        public object? Highway { get; set; }
    }
}

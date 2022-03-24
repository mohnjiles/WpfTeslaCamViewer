using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfTeslaCamViewer.Converters;

namespace WpfTeslaCamViewer.UnitTests
{
    [TestClass]
    public class TeslaCamReasonConverterTests
    {
        [TestMethod]
        [DataRow("user_interaction_dashcam_panel_save")]
        [DataRow("user_interaction_dashcam_icon_tapped")]
        [DataRow("user_interaction_honk")]
        [DataRow("sentry_aware_object_detection")]
        [DataRow("sentry_aware_accel")]
        [DataRow("sentry_aware_accel_12345_68-9834")] // idk what the numbers look like but yolo
        public void ReadJson_ValidTeslaCamReason_ReturnsEnumValue(string testReason)
        {
            // Arrange
            var testJson =
                $"{{\"timestamp\":\"2021-11-18T12:09:26\",\"city\":\"Somewhere\",\"est_lat\":\"11.3493\",\"est_lon\":\"142.1821\",\"reason\":\"{testReason}\",\"camera\":\"0\"}}";
            
            // Act
            var result = testJson.Deserialize<TeslaCamEventInfo>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(TeslaCamReason.Unknown, result.Reason, "Reason should not be Unknown");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        [DataRow("yolo")]
        [DataRow("elon_musk_sighting")]
        public void ReadJson_InvalidTeslaCamReason_ReturnsUnknown(string testReason)
        {
            // Arrange
            var testJson =
                $"{{\"timestamp\":\"2021-11-18T12:09:26\",\"city\":\"Somewhere\",\"est_lat\":\"11.3493\",\"est_lon\":\"142.1821\",\"reason\":\"{testReason}\",\"camera\":\"0\"}}";

            // Act
            var result = testJson.Deserialize<TeslaCamEventInfo>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TeslaCamReason.Unknown, result.Reason, "Reason should be Unknown");
        }
    }
}
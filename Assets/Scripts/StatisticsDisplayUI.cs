using System.Text;
using TMPro;
using UnityEngine;
using Zenject;

public class StatisticsDisplayUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_statisticsText;

    private PlayerStatistics m_playerStatistics;
    private SignalBus m_signalBus;

    [Inject]
    private void Construct(PlayerStatistics playerStatistics, SignalBus signalBus)
    {
        m_playerStatistics = playerStatistics;
        m_signalBus = signalBus;
    }

    private void OnEnable()
    {
        m_signalBus.Subscribe<PlayerStatisticsCalculatedSignal>(OnStatisticsRecalculated);

        RefreshStatistics(m_playerStatistics.Attributes);
    }

    private void OnDisable()
    {
        m_signalBus.Unsubscribe<PlayerStatisticsCalculatedSignal>(OnStatisticsRecalculated);
    }

    public void RefreshStatistics(PlayerAttributes playerAttributes)
    {
        StringBuilder stringBuilder = new StringBuilder();

        AddStatisticText(stringBuilder, "Move Speed", playerAttributes.MoveSpeed, playerAttributes.BaseMoveSpeed, playerAttributes.MoveSpeedModifier, true);
        AddStatisticText(stringBuilder, "Damage", playerAttributes.Damage, playerAttributes.BaseDamage, playerAttributes.AdditionalDamage, false);

        m_statisticsText.text = stringBuilder.ToString();
    }

    public void AddStatisticText(StringBuilder stringBuilder, string attributeName, float totalValue, float baseValue, float additionalValue, bool isPercentage)
    {
        stringBuilder.Append(attributeName);
        stringBuilder.Append(": ");

        string valueFormat = isPercentage ? "{0:F2}%" : "{0:F2}";

        stringBuilder.Append(string.Format(valueFormat, totalValue));

        stringBuilder.Append(" = ");
        stringBuilder.Append(baseValue.ToString("F2"));
        stringBuilder.Append(" + ");
        stringBuilder.Append(string.Format(valueFormat, additionalValue));

        stringBuilder.AppendLine();
    }

    private void OnStatisticsRecalculated(PlayerStatisticsCalculatedSignal calculatedSignal)
    {
        RefreshStatistics(calculatedSignal.PlayerAttributes);
    }
}

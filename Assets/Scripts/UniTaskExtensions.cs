using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

/// <summary>
///  ласс-расширение дл€ UniTask
/// </summary>
public static class UniTaskExtensions
{
    /// <summary>
    /// —писок всех токенов, добавленных дл€ удалени€
    /// </summary>
    private static readonly List<CancellationTokenSource> tokenSources = new List<CancellationTokenSource>();

    /// <summary>
    /// ћетод, позвол€ющий запустить UniTask с возможностью глобальной отмены
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationTokenSource">CancellationTokenSource, используемый дл€ отмены UniTask</param>
    /// <returns></returns>
    public static UniTask RunWithCancellation(this UniTask task, CancellationTokenSource cancellationTokenSource)
    {
        tokenSources.Add(cancellationTokenSource);

        return task.AttachExternalCancellation(cancellationTokenSource.Token);
    }

    /// <summary>
    /// ћетод, позвол€ющий остановить все добавленные UniTask
    /// </summary>
    public static void CancelAll()
    {
        foreach (var cts in tokenSources)
        {
            cts.Cancel();
        }

        tokenSources.Clear();
    }
}

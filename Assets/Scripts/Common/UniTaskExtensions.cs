using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// �����-���������� ��� UniTask
/// </summary>
public static class UniTaskExtensions
{
    /// <summary>
    /// ������ ���� �������, ����������� ��� ��������
    /// </summary>
    private static readonly List<CancellationTokenSource> tokenSources = new List<CancellationTokenSource>();

    /// <summary>
    /// �����, ����������� ��������� UniTask � ������������ ���������� ������
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationTokenSource">CancellationTokenSource, ������������ ��� ������ UniTask</param>
    /// <returns></returns>
    public static UniTask RunWithCancellation(this UniTask task, CancellationTokenSource cancellationTokenSource)
    {
        tokenSources.Add(cancellationTokenSource);

        return task.AttachExternalCancellation(cancellationTokenSource.Token);
    }

    /// <summary>
    /// �����, ����������� ���������� ��� ����������� UniTask
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

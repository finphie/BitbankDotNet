﻿using System;
using System.Collections.Generic;
using System.Net;

namespace BitbankDotNet
{
    /// <summary>
    /// <see cref="BitbankClient"/>例外クラス
    /// </summary>
    public class BitbankApiException : Exception
    {
        /// <summary>
        /// エラーコード一覧
        /// cf. https://docs.bitbank.cc/error_code/
        /// </summary>
        static readonly Dictionary<int, string> ErrorCodes = new Dictionary<int, string>
        {
            {10000, "URLが存在しません。"},
            {10001, "システムエラーが発生しました。サポートにお問い合わせ下さい。"},
            {10002, "不正なJSON形式です。送信内容をご確認下さい。"},
            {10003, "システムエラーが発生しました。サポートにお問い合わせ下さい。"},
            {10005, "タイムアウトエラーが発生しました。しばらく間をおいて再度実行して下さい。"},
            {20001, "API認証に失敗しました。"},
            {20002, "APIキーが不正です。"},
            {20003, "APIキーが存在しません。"},
            {20004, "API Nonceが存在しません。"},
            {20005, "APIシグネチャが存在しません。"},
            {20011, "２段階認証に失敗しました。"},
            {20014, "SMS認証に失敗しました。"},
            {30001, "注文数量を指定して下さい。"},
            {30006, "注文IDを指定して下さい。"},
            {30007, "注文ID配列を指定して下さい。"},
            {30009, "銘柄を指定して下さい。"},
            {30012, "注文価格を指定して下さい。"},
            {30013, "売買どちらかを指定して下さい。"},
            {30015, "注文タイプを指定して下さい。"},
            {30016, "アセット名を指定して下さい。"},
            {30019, "uuidを指定して下さい。"},
            {30039, "出金額を指定して下さい。"},
            {40001, "注文数量が不正です。"},
            {40006, "count値が不正です。"},
            {40007, "終了時期が不正です。"},
            {40008, "end_id値が不正です。"},
            {40009, "from_id値が不正です。"},
            {40013, "注文IDが不正です。"},
            {40014, "注文ID配列が不正です。"},
            {40015, "指定された注文が多すぎます。"},
            {40017, "銘柄名が不正です。"},
            {40020, "注文価格が不正です。"},
            {40021, "売買区分が不正です。"},
            {40022, "開始時期が不正です。"},
            {40024, "注文タイプが不正です。"},
            {40025, "アセット名が不正です。"},
            {40028, "uuidが不正です。"},
            {40048, "出金額が不正です。"},
            {50003, "現在、このアカウントはご指定の操作を実行できない状態となっております。サポートにお問い合わせ下さい。"},
            {50004, "現在、このアカウントは仮登録の状態となっております。アカウント登録完了後、再度お試し下さい。"},
            {50005, "現在、このアカウントはロックされております。サポートにお問い合わせ下さい。"},
            {50006, "現在、このアカウントはロックされております。サポートにお問い合わせ下さい。"},
            {50008, "ユーザの本人確認が完了していません。"},
            {50009, "ご指定の注文は存在しません。"},
            {50010, "ご指定の注文はキャンセルできません。"},
            {50011, "APIが見つかりません。"},
            {50026, "ご指定の注文は既にキャンセル済みです。"},
            {50027, "ご指定の注文は既に約定済みです。"},
            {60001, "保有数量が不足しています。"},
            {60002, "成行買い注文の数量上限を上回っています。"},
            {60003, "指定した数量が制限を超えています。"},
            {60004, "指定した数量がしきい値を下回っています。"},
            {60005, "指定した価格が上限を上回っています。"},
            {60006, "指定した価格が下限を下回っています。"},
            {60011, "同時発注制限件数(30件)を上回っています。"},
            {70001, "システムエラーが発生しました。サポートにお問い合わせ下さい。"},
            {70002, "システムエラーが発生しました。サポートにお問い合わせ下さい。"},
            {70003, "システムエラーが発生しました。サポートにお問い合わせ下さい。"},
            {70004, "現在取引停止中のため、注文を承ることができません。"},
            {70005, "現在買注文停止中のため、注文を承ることができません。"},
            {70006, "現在売注文停止中のため、注文を承ることができません。"},
            {70009, "ただいま成行注文を一時的に制限しています。指値注文をご利用ください。。"},
            {70010, "ただいまシステム負荷が高まっているため、最小注文数量を一時的に引き上げています。"},
            {70011, "ただいまリクエストが混雑してます。しばらく時間を空けてから再度リクエストをお願いします。"}
        };

        /// <summary>
        /// HTTPステータス
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// BitbankAPIのエラーコード
        /// </summary>
        public int ApiErrorCode { get; }

        /// <summary>
        /// Bitbank APIのエラーメッセージ
        /// </summary>
        public string ErrorMessage { get; }

        public BitbankApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public BitbankApiException(string message, Exception inner, HttpStatusCode statusCode)
            : base(message, inner)
            => StatusCode = statusCode;

        public BitbankApiException(string message, HttpStatusCode statusCode, int apiErrorCode)
            : this(message, null, statusCode)
        {
            ApiErrorCode = apiErrorCode;
            ErrorCodes.TryGetValue(apiErrorCode, out var errorMessage);
            ErrorMessage = errorMessage;
        }
    }
}
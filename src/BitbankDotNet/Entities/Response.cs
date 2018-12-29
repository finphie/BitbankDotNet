namespace BitbankDotNet.Entities
{
    /// <summary>
    /// APIレスポンスの基底クラス
    /// </summary>
    /// <typeparam name="T"><see cref="Entities"/>名前空間内のクラス</typeparam>
    class Response<T>
        where T : class
    {
        /// <summary>
        /// APIリクエストに成功
        /// </summary>
        public int Success { get; set; }

        /// <summary>
        /// Jsonデータ
        /// </summary>
        public T Data { get; set; }
    }
}
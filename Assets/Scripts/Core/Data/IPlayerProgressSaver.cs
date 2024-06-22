namespace Core.Data
{
    public interface IPlayerProgressSaver
    {
        void SaveData(PlayerData playerData);
        PlayerData LoadData();
    }
}
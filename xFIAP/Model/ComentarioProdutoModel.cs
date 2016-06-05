using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFIAP.Model
{
    public class ComentarioProdutoModel
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string Produto
        {
            get;
            set;
        }
        public string Comentario
        {
            get;
            set;
        }
    }

    public static class ComentarioProdutoRepository
    {
        public static void CriarComentario(ComentarioProdutoModel objComentario)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "ComentarioDb.sqlite");
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objComentario);
                });
            }
        }

        public static async Task<ObservableCollection<ComentarioProdutoModel>> GetComentariosAsync()
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "ComentarioDb.sqlite");
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                List<ComentarioProdutoModel> myCollection = conn.Table<ComentarioProdutoModel>().ToList<ComentarioProdutoModel>();
                ObservableCollection<ComentarioProdutoModel> ComentarioList = new ObservableCollection<ComentarioProdutoModel>(myCollection);
                return ComentarioList;
            }
        }
    }
}

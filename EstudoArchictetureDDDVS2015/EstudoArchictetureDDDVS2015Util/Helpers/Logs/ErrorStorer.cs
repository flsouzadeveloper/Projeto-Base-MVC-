using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EstudoArchictetureDDDVS2015Util.Helpers.Logs
{
    public abstract class ErrorStorer
    {
        #region Singleton

        public static ErrorStorer Storer { get; private set; }

        public static void LoadSingleton(ErrorStorer storer)
        {
            Storer = storer;
        }

        #endregion

        #region Fields

        private static object _lock = new object();

        #endregion

        #region Properties

        public abstract string FolderPath { get; }

        #endregion

        #region Methods

        protected abstract string GetContext();

        public void Create(Exception ex, bool supressError = true)
        {
            try
            {
                var context = GetContext();

                //var error = new ErrorInfo(context, ex);

                //using (var fs = CreateUniqueFile(error.OccurredAt))
                //{
                //    error.Save(fs);
                //}
            }
            catch
            {
                if (!supressError) throw;
            }
        }

        //public ErrorInfo Read(string fileIdentifier)
        //{
        //    var fullName = Path.Combine(FolderPath, fileIdentifier + ".xml");

        //    return ErrorInfo.Load(fullName);
        //}

        public void Delete(string fileIdentifier)
        {
            var fullName = Path.Combine(FolderPath, fileIdentifier + ".xml");

            File.Delete(fullName);
        }

        public List<ErrorStored> List(DateTime? start, DateTime? end)
        {
            if (start != null) start = start.Value.Date;
            if (end != null) end = end.Value.Date.AddDays(1).AddMilliseconds(-1);

            // filtra primeiro pelo nome do arquivo antes de carregar.
            return Directory.GetFiles(FolderPath)
                .Select(s => new { FileName = s, Date = GetDateTimeFromFileName(s) })
                .Where(w => start == null || w.Date >= start)
                .Where(w => end == null || w.Date <= end)
                .Select(s => new ErrorStored
                {
                    //ErrorInfo = ErrorInfo.Load(s.FileName),
                    Identifier = GetFileIdentifierFromFileName(s.FileName),
                    FileName = s.FileName
                })
                .ToList();
        }

        #endregion

        #region Support Methods

        private static string GetFileIdentifierFromFileName(string fullFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(fullFileName);

            return Regex.Match(fileName, @"^\d{4}(_\d{2}){5}_\d{7}__\d{3}$").Value;
        }

        private static DateTime GetDateTimeFromFileName(string fullFileName)
        {
            return GetDateTimeFromIdentifier(GetFileIdentifierFromFileName(fullFileName));
        }

        private static DateTime GetDateTimeFromIdentifier(string fileIdentifier)
        {
            var ticks = new DateTime(
                int.Parse(fileIdentifier.Substring(0, 4)), // year
                int.Parse(fileIdentifier.Substring(5, 2)), // month
                int.Parse(fileIdentifier.Substring(8, 2)), // day
                int.Parse(fileIdentifier.Substring(11, 2)), // hour
                int.Parse(fileIdentifier.Substring(14, 2)), // minute
                int.Parse(fileIdentifier.Substring(17, 2))).Ticks + // second
                int.Parse(fileIdentifier.Substring(20, 7)); // extra ticks

            return new DateTime(ticks);
        }

        private FileStream CreateUniqueFile(DateTime dateTime)
        {
            lock (_lock)
            {
                var index = 0;
                string fullName;

                do
                {
                    var fileName = string.Format("{0:yyyy'_'MM'_'dd'_'HH'_'mm'_'ss'_'fffffff}__{1:000}.xml", dateTime, index);

                    fullName = Path.Combine(FolderPath, fileName);

                    index++;

                } while (File.Exists(fullName));

                if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);

                return File.Create(fullName);
            }
        }

        #endregion
    }
}

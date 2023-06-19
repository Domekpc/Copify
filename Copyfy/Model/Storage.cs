using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyfy.Model
{
    public class Storage<T> 
    {
        public List<T> items { get; }
        
        #region Element access
        public int Count
        {
            get { return items.Count; }
        }
        public T this[int index]
        {
            get { return items[index]; }
        }
        public T[] ToArray()
        {
            return items.ToArray();
        }
        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }
        public void Clear() 
        { 
            items.Clear();
        }
        public T Find(Predicate<T> match)
        {
            return items.Find(match);
        }
        public void Remove(T item)
        {
            items.Remove(item);
        }
        public bool Contains(T item)
        {
            return items.Contains(item);
        }
        #endregion

        public Storage()
        {
            items = new List<T>();
        }

        #region Add elements

        public void Add(T newItem)
        {
            items.Add(newItem);
        }

        public void LoadFromFile(string fileName, Func<string, T> parser)
        {
            
            string data = "";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream && sr != null)
                    {
                        data = sr.ReadLine();
                       
                        items.Add(parser(data));
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadFromDirInfo(string fileName, Func<string, T> parser)
        {
            DirectoryInfo directoryInfo = null;
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream && sr != null)
                    {
                        directoryInfo = new DirectoryInfo(sr.ReadLine());
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
          
            try
            {
                foreach (var dir in directoryInfo.GetDirectories())
                {
                    items.Add(parser(dir.FullName));
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadFromFileSeparately(string fileName, Func<string[], T> parser, bool automated = false)
        {
            items.Clear();
           
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream && sr != null)
                    {
                        string data = sr.ReadLine();
                        if (!data.Any(char.IsLetter))
                        {
                            break;
                        }
                        string[] words = data.Split(';');

                        items.Add(parser(words));
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                if(!automated)
                    MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                if (!automated)
                    MessageBox.Show(ex.Message);
            }
            catch (IOException ex)
            {
                if (!automated)
                    MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}


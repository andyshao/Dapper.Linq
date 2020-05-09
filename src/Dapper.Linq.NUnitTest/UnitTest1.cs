using Daper.Entitys;
using NUnit.Framework;

namespace Dapper.Linq.NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var db = new DbContext(new DbContextBuilder
            {
                Connection = new MySql.Data.MySqlClient.MySqlConnection("server=127.0.0.1;user id=root;password=1024;database=test;")
            });
            db.Open();
            var student = db.From<Student>().Get(1);
            var list = db.From<Student>()
                .Where(a => a.Id > 1)
                .Select();
            Assert.Pass();
        }

        public void Test2()
        {
            //һ��Ӧ��ֻ��Ҫ����һ��ʵ��
            var resovle = new XmlResovle();
            //����ָ��xml·����������
            resovle.Load(@"D:\Dapper.Linq\src\Dapper.Linq.NUnitTest\student.xml");
            var db = new DbContext(new DbContextBuilder
            {
                Connection = new MySql.Data.MySqlClient.MySqlConnection("server=127.0.0.1;user id=root;password=1024;database=test;")
            });
            db.Open();
            var student = db.From<Student>().Get(1);
            var list = db.From<Student>()
                .Where(a => a.Id > 1)
                .Select();
            //���ڵײ��������ʽ�ǻ��ڱ���ʽ��ʵ�ֵģ����Id��Age��������һ������
            //1.�������Ϊnull����Ϊ����ʽ��Id!=null,�����Id>0����Id������int���ͣ�
            //2.������public��
            using (var multi = db.From("student.list", new { Id = (int?)null, Age = (int?)90 }).ExecuteMultiQuery())
            {
                //ִ�е�һ��sql
                var list2 = multi.GetList<Student>();
                //ִ�еڶ���sql
                var count = multi.Get<int>();
            }
        }
    }
}
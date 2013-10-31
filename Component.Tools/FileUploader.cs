using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Component.Tools
{
    /// <summary>
    ///     通用上传类
    /// </summary>
    public class FileUploader
    {
        string state = "SUCCESS";

        string URL = null;
        string currentType = null;
        string uploadpath = null;
        string filename = null;
        string originalName = null;
        HttpPostedFile uploadFile = null;

        /// <summary>
        ///     上传文件的主处理方法
        /// </summary>
        /// <param name="cxt"></param>
        /// <param name="pathbase"></param>
        /// <param name="filetype"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public  Hashtable upFileImage(HttpContext cxt, string pathbase, string[] filetype, int size)
        {
            pathbase = pathbase + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            uploadpath = cxt.Server.MapPath(pathbase);//获取文件上传路径

            try
            {
                uploadFile = cxt.Request.Files[0];
                originalName = uploadFile.FileName;

                //目录创建
                createFolder();

                //格式验证
                if (checkType(filetype))
                {
                    state = "不允许的文件类型";
                }
                //大小验证
                if (checkSize(size))
                {
                    state = "文件大小超出网站限制";
                }
                //保存图片
                if (state == "SUCCESS")
                {
                    filename = reName();
                    //uploadFile.SaveAs(uploadpath + filename);
                    ZoomAuto(uploadFile.InputStream, 600, 345);
                    URL = pathbase + filename;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return getUploadInfo();
        }
        /// <summary>
        /// 获取上传信息
        /// </summary>
        /// <returns></returns>
        private Hashtable getUploadInfo()
        {
            Hashtable infoList = new Hashtable();

            infoList.Add("state", state);
            infoList.Add("url", URL);

            if (currentType != null)
                infoList.Add("currentType", currentType);
            if (originalName != null)
                infoList.Add("originalName", originalName);
            return infoList;
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="cxt"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string getOtherInfo(HttpContext cxt, string field)
        {
            string info = null;
            if (cxt.Request.Form[field] != null && !String.IsNullOrEmpty(cxt.Request.Form[field]))
            {
                info = field == "fileName" ? cxt.Request.Form[field].Split(',')[1] : cxt.Request.Form[field];
            }
            return info;
        }


        #region 等比缩放

        ///<summary>
        ///图片等比缩放
        ///</summary>
        ///<param name="fromFile">原图Stream对象</param>
        ///<param name="savePath">缩略图存放地址</param>
        ///<param name="targetWidth">指定的最大宽度</param>
        ///<param name="targetHeight">指定的最大高度</param>
        ///<param name="watermarkText">水印文字(为""表示不使用水印)</param>
        ///<param name="watermarkImage">水印图片路径(为""表示不使用水印)</param>
        public void ZoomAuto(Stream fromFile, double targetWidth, double targetHeight)
        {
            //返回图片名称
            //string imageName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";

            //创建目录
            //string dir = Path.GetDirectoryName(savePath);
            //if (!Directory.Exists(dir))
            //{
            //    Directory.CreateDirectory(dir);
           // }

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            Image initImage = Image.FromStream(fromFile, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
            {
                initImage.Save(uploadpath + filename);
            }
            else
            {
                //缩略图宽、高计算
                double newWidth = initImage.Width;
                double newHeight = initImage.Height;

                //宽大于高或宽等于高（横图或正方）
                if (initImage.Width > initImage.Height || initImage.Width == newHeight)
                {
                    //如果宽大于模版
                    if (initImage.Width > targetWidth)
                    {
                        //宽按模版，高按比例缩放
                        newWidth = targetWidth;
                        newHeight = initImage.Height * (targetWidth / initImage.Width);
                    }
                }
                else//高大于宽（竖图）
                {
                    //如果高大于模版
                    if (initImage.Height > targetHeight)
                    {
                        //高按模版，宽按比例缩放
                        newHeight = targetHeight;
                        newWidth = initImage.Width * (targetHeight / initImage.Height);
                    }
                }

                //生成新图
                //新建一个bmp图片
                Image newImage = new Bitmap((int)newWidth, (int)newHeight);

                //新建一个画板
                Graphics newG = Graphics.FromImage(newImage);

                //设置质量
                newG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = SmoothingMode.HighQuality;

                //置背景色
                newG.Clear(Color.White);

                //画图
                newG.DrawImage(initImage, new Rectangle(0, 0, newImage.Width, newImage.Height), new Rectangle(0, 0, initImage.Width, initImage.Height), GraphicsUnit.Pixel);

                //保存缩略图
                newImage.Save(uploadpath + filename);

                //释放资源
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
            }

        }
        #endregion
        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <returns></returns>
        private string reName()
        {
            return Guid.NewGuid() + getFileExt();
        }
        /// <summary>
        /// 文件大小检测
        /// </summary>
        /// <param name="size">大小M为单位</param>
        /// <returns></returns>
        private bool checkSize(int size)
        {
            return uploadFile.ContentLength > (size * 1024 * 1024);
        }
        /// <summary>
        /// 文件类型检测
        /// </summary>
        /// <param name="filetype"></param>
        /// <returns></returns>
        private bool checkType(string[] filetype)
        {
            currentType = getFileExt();
            return Array.IndexOf(filetype, currentType) == -1;
        }
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <returns>文件扩展名</returns>
        private string getFileExt()
        {
            string[] temp = uploadFile.FileName.Split('.');
            return "." + temp[temp.Length - 1].ToLower();
        }
        /// <summary>
        /// 按照日期自动创建存储文件夹
        /// </summary>
        private void createFolder()
        {
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
        }
        /// <summary>
        ///     删除存储文件夹
        /// </summary>
        /// <param name="path"></param>
        public void deleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}

using Application.contract;
using AutoMapper;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.Lecture;
using BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.IO;

namespace Application.Services
{
    public class LectureService: ILectureService
    {
       
            private readonly IUniteOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IWebHostEnvironment _environment;

            public LectureService(IUniteOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _environment = environment;
            }
        public async Task<Lecture> CreateLectureAsync(CreateLecture dto)
        {
            // 1. إنشاء المحاضرة
            var lecture = new Lecture
            {
                ScheduleLectureId = dto.ScheduleLectureId,
               
            };

            await _unitOfWork.Lectures.AddAsync(lecture);
                  _unitOfWork.save();

            // 2. توليد QR Code يحتوي على Id
            string qrText = lecture.Id.ToString();

            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrData);
            byte[] qrBytes = qrCode.GetGraphic(20);

            // 3. حفظ الصورة
            string fileName = $"lecture_{lecture.Id}.png";
            string savePath = Path.Combine("wwwroot/QRCodeImage", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!); // لو مش موجود
            File.WriteAllBytes(savePath, qrBytes);

            // 4. تحديث المحاضرة بالمسار
            lecture.QrCodeImgPath = $"/QRCodeImage/{fileName}";
            _unitOfWork.Lectures.Update(lecture);
           int r= _unitOfWork.save();
            if (r > 0) { return lecture; }
            else return null;
        }



        public async Task<string> GetQrCodeAsync(Guid lectureId)
            {
                var lecture = await _unitOfWork.Lectures.GetByIdAsync(lectureId);
                return  lecture?.QrCodeImgPath;
            }


        public async Task<string> GenerateAndSaveQrCodeAsync(string lectureTokenOrId)
        {
            // 1. توليد QR Code
            using var qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(lectureTokenOrId, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            // 2. تحديد مسار الحفظ
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "QRCodeImage");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = $"QR_{Guid.NewGuid()}.png";
            string filePath = Path.Combine(folderPath, fileName);

            // 3. حفظ الصورة
            await File.WriteAllBytesAsync(filePath, qrCodeBytes);

            // 4. حفظ الـ Path في الـ DB (مثلاً ترجعي فقط الـ relative path)
            string relativePath = Path.Combine("QRCodeImage", fileName);
            return relativePath;
        }

    }
}

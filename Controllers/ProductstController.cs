using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_hero.Controllers
{
    [ApiController]
    [Route("[controller]")] //https://localhost:5001/Products (dev)
    public class ProductsController : ControllerBase //inheritance
    {
        [HttpGet("IActionResult")]//https://localhost:5001/Products/IActionResult
        public IActionResult GetProductsIActionResult()
        {
            var products = new List<string>();
            products.Add("Samsung");
            products.Add("Nokia");
            return Ok(products);// status Ok = 200
        }
        //{} = requried
        [HttpGet("{Id}")]//https://localhost:5001/Products/ActionResult/int Id (ทำการค้นหาจาก Id)
        public IActionResult GetProductById(int Id)//from required ถ้าต้องการไม่ให้มีค่าว่างใช้แบบนี้
        {
            return Ok(new {productId = Id, name = "samsung"});
            //productId นำค่าที่ได้มาจาก parameter int Id และ name กำหนดค่าให้เป็น sansung (productId และ name คือ object)
        }
        
        [HttpGet("search")]//https://localhost:5001/Products/search?names=porawit (names = คือค่าที่พิมพ์ใส่เข้าไป)
        public IActionResult SearchProducts([FromQuery] string names)//from query มีค่าเป็น null ได้
        {
            return Ok(new {productId = 111, name = names});
        }

        //models

        public class Products //poco class
        {
            public int Id { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
        }

        // [HttpPost("fromJson")] //https://localhost:5001/Product (from json)
        // public IActionResult AddProductfromJson(Products model)
        // {
        //     return Ok(model);//แสดงข้อมูลที่เพิ่มเข้าไป
        // }

        [HttpPost("fromFrom")] //https://localhost:5001/Product (from from)
        public IActionResult AddProductfromFrom([FromForm]Products model)
        {
            //CreatedAtAction status 204 เป็น status ที่เอาไว้สำหรับเพิ่มข้อมูล
            return CreatedAtAction(nameof(GetProductById),new {Id = model.Id}, model);
            //nameof ทำการอ้างอิงไปที่ method GetProductById 
            //กำหนดให้ Id เป็น object ให้มีค่าเท่ากับ model.Id
            //object ตัวสุดท้ายคือ model เอาไว้แสดงข้อมูลที่ได้เพิ่มเข้าไปทั้งหมด
            
        }
        //{} = requried
        [HttpPut("{Id}")] //https://localhost:5001/Product/{Id}
        public IActionResult UpdataProduct(int Id, [FromForm] Products model)
        {
            if (Id != model.Id)//กรณีที่ Id maping และ Id model ไม่ตรงกัน
            {
                return BadRequest();//status 400 (มีปัญหาทางฝั่งผู้ใช้งาน)
            }
            else if (Id != 1150)//กรณีค้าหา Id ที่ป้อนมาไม่ตรงกับ 1150
            {
                return NotFound();//status 404
            }
            return CreatedAtAction(nameof(UpdataProduct),Id,model);
            //nameof ทำการอ้างอิงไปที่ method UpdataProduct
        }

        [HttpDelete("{Id}")]//{} = requride
        //https://localhost:5001/Product/{id}
        public IActionResult DeleteProduct(int Id)
        {
            if (Id != 1150)//ผู้ใช้งานป้อนค่าไม่ตรงกับ 1150 
            {
                return BadRequest();//แสดง status 400
            }
            //ผู้ใช้งานป้อน 1150 
            return NoContent();//แสดง status 204 ทำการลบเรียบร้อย
        }

        // [HttpGet("ActionResult")]
        // public ActionResult<List<string>> GetProductsActionResult()
        // {
        //     var products = new List<string>();
        //     products.Add("IMax");
        //     products.Add("Iphone");
        //     return products;
        // }

        // Test
        // //https://localhost:5001/Products (get)
        // [HttpGet] //http method: get, post, put, delete
        // public IEnumerable<string> Get() //action
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => $"index: {index}")
        //     .ToArray();
        // }

        // //https://localhost:5001/Products/name (get)
        // [HttpGet("name")] //http method: get, post, put, delete
        // public IEnumerable<string> Get1() //action
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 50).Select(index => $"index: {index}")
        //     .ToArray();
        // }
    }
}

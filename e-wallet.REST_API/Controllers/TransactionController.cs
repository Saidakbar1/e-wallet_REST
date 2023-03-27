using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_wallet.Data.Model;
using e_wallet.REST_API.DataContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace e_wallet.REST_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly EWalletContext _context;

        public TransactionController(EWalletContext context)
        {
            _context = context;
        }

        // PUT: Replenish e-wallet account
        [HttpPut]
        public async Task<IActionResult> ReplenishAmount(Guid userId, double amount)
        {
            var userToChange = _context.Users.Where(UserId => UserId.Id == userId).FirstOrDefault();
            if (userToChange == null) 
            {
                throw new ArgumentException("User not found");
            }
            if (userToChange.Identified)
            {

                if ((userToChange.Balance + amount) <= 100000)
                {
                    userToChange.Balance += amount;
                }
                else
                {
                    throw new ArgumentException("Balance will exceeds 100000!");
                }
            }
            else
            {
                if ((userToChange.Balance + amount) <= 10000)
                {
                    userToChange.Balance += amount;
                }
                else
                {
                    throw new ArgumentException("Balance will exceeds 10000!");
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: Get total number and amount of recharge
        [HttpGet]
        public async Task<IActionResult> GetHistories(Guid userId)
        {
            var totalNumber = _context.Transactions.Where(transaction => transaction.UserId == userId && transaction.Date >= DateTime.Now.AddDays(-30)).Count();
            return Ok();
        }

        // GET: Get e-wallet balance
        [HttpGet]
        public double GetBalance(Guid userId)
        {
            var userToReturn = _context.Users.Where(UserId => UserId.Id == userId).FirstOrDefault();
            if (userToReturn == null)
            {
                throw new ArgumentException("User not found");
            }
            return userToReturn.Balance;
        }

        //// PUT: api/Transactions/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTransaction(Guid id, Transaction transaction)
        //{
        //    if (id != transaction.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(transaction).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TransactionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        //{
        //    if (_context.Transactions == null)
        //    {
        //        return Problem("Entity set 'EWalletContext.Histories'  is null.");
        //    }
        //    _context.Transactions.Add(transaction);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        //}

        //// DELETE: api/Transactions/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTransaction(Guid id)
        //{
        //    if (_context.Transactions == null)
        //    {
        //        return NotFound();
        //    }
        //    var transaction = await _context.Transactions.FindAsync(id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Transactions.Remove(transaction);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TransactionExists(Guid id)
        {
            return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

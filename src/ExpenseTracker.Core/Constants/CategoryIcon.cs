using System.Collections.Generic;

namespace ExpenseTracker.Core.Constants
{
    public static class CategoryIcon
    {
        public static readonly Dictionary<string, string> Icons = new()
        {
            {
                "Archive",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-archive' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
                <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
                <rect x='3' y='4' width='18' height='4' rx='2' />
                <path d='M5 8v10a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-10' />
                <line x1='10' y1='12' x2='14' y2='12' />
              </svg>"
            },
            {
                "Ambulance",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-ambulance' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
                <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
                <circle cx='7' cy='17' r='2' />
                <circle cx='17' cy='17' r='2' />
                <path d='M5 17h-2v-11a1 1 0 0 1 1 -1h9v12m-4 0h6m4 0h2v-6h-8m0 -5h5l3 5' />
                <path d='M6 10h4m-2 -2v4' /></svg>"
            },
            {
                "Food",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-apple' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
                <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
                <circle cx='12' cy='14' r='7' />
                <path d='M12 11v-6a2 2 0 0 1 2 -2h2v1a2 2 0 0 1 -2 2h-2' />
                <path d='M10 10.5c1.333 .667 2.667 .667 4 0' /></svg>"
            },
            {
                "Basket Ball",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-ball-basketball' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
                <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
                <circle cx='12' cy='12' r='9' />
                <line x1='5.65' y1='5.65' x2='18.35' y2='18.35' />
                <line x1='5.65' y1='18.35' x2='18.35' y2='5.65' />
                <path d='M12 3a9 9 0 0 0 9 9' />
                <path d='M3 12a9 9 0 0 1 9 9' /></svg>"
            },
            {
                "Atom",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-atom' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
                <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
                <line x1='12' y1='12' x2='12' y2='12.01' />
                <path d='M12 2a4 10 0 0 0 -4 10a4 10 0 0 0 4 10a4 10 0 0 0 4 -10a4 10 0 0 0 -4 -10' transform='rotate(45 12 12)' />
                <path d='M12 2a4 10 0 0 0 -4 10a4 10 0 0 0 4 10a4 10 0 0 0 4 -10a4 10 0 0 0 -4 -10' transform='rotate(-45 12 12)' /></svg>"
            },
            {
                "Shopping",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-basket' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <polyline points='7 10 12 4 17 10' />
  <path d='M21 10l-2 8a2 2.5 0 0 1 -2 2h-10a2 2.5 0 0 1 -2 -2l-2 -8z' />
  <circle cx='12' cy='15' r='2' /></svg>"
            },
            {
                "Bike",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-bike' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <circle cx='5' cy='18' r='3' />
  <circle cx='19' cy='18' r='3' />
  <polyline points='12 19 12 15 9 12 14 8 16 11 19 11' />
  <circle cx='17' cy='5' r='1' /></svg>"
            },
            {
                "Book",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-book' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M3 19a9 9 0 0 1 9 0a9 9 0 0 1 9 0' />
  <path d='M3 6a9 9 0 0 1 9 0a9 9 0 0 1 9 0' />
  <line x1='3' y1='6' x2='3' y2='19' />
  <line x1='12' y1='6' x2='12' y2='19' />
  <line x1='21' y1='6' x2='21' y2='19' /></svg>"
            },
            {
                "Bottle",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-bottle' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M10 5h4v-2a1 1 0 0 0 -1 -1h-2a1 1 0 0 0 -1 1v2z' />
  <path d='M14 3.5c0 1.626 .507 3.212 1.45 4.537l.05 .07a8.093 8.093 0 0 1 1.5 4.694v6.199a2 2 0 0 1 -2 2h-6a2 2 0 0 1 -2 -2v-6.2c0 -1.682 .524 -3.322 1.5 -4.693l.05 -.07a7.823 7.823 0 0 0 1.45 -4.537' />
  <path d='M7.003 14.803a2.4 2.4 0 0 0 .997 -.803a2.4 2.4 0 0 1 2 -1a2.4 2.4 0 0 1 2 1a2.4 2.4 0 0 0 2 1a2.4 2.4 0 0 0 2 -1a2.4 2.4 0 0 1 1 -.805' /></svg>"
            },
            {
                "Cash",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-car' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <circle cx='7' cy='17' r='2' />
  <circle cx='17' cy='17' r='2' />
  <path d='M5 17h-2v-6l2 -5h9l4 5h1a2 2 0 0 1 2 2v4h-2m-4 0h-6m-6 -6h15m-6 0v-5' /></svg>"
            },
            {
                "Laptop",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-device-laptop' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <line x1='3' y1='19' x2='21' y2='19' />
  <rect x='5' y='6' width='14' height='10' rx='1' /></svg>"
            },
            {
                "Mobile",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-device-mobile' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <rect x='7' y='4' width='10' height='16' rx='1' />
  <line x1='11' y1='5' x2='13' y2='5' />
  <line x1='12' y1='17' x2='12' y2='17.01' /></svg>"
            },
            {
                "Watch",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-device-watch' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <rect x='6' y='6' width='12' height='12' rx='3' />
  <path d='M9 18v3h6v-3' />
  <path d='M9 6v-3h6v3' /></svg>"
            },
            {
                "Discount",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-discount-2' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <line x1='9' y1='15' x2='15' y2='9' />
  <circle cx='9.5' cy='9.5' r='.5' fill='currentColor' />
  <circle cx='14.5' cy='14.5' r='.5' fill='currentColor' />
  <path d='M5 7.2a2.2 2.2 0 0 1 2.2 -2.2h1a2.2 2.2 0 0 0 1.55 -.64l.7 -.7a2.2 2.2 0 0 1 3.12 0l.7 .7a2.2 2.2 0 0 0 1.55 .64h1a2.2 2.2 0 0 1 2.2 2.2v1a2.2 2.2 0 0 0 .64 1.55l.7 .7a2.2 2.2 0 0 1 0 3.12l-.7 .7a2.2 2.2 0 0 0 -.64 1.55v1a2.2 2.2 0 0 1 -2.2 2.2h-1a2.2 2.2 0 0 0 -1.55 .64l-.7 .7a2.2 2.2 0 0 1 -3.12 0l-.7 -.7a2.2 2.2 0 0 0 -1.55 -.64h-1a2.2 2.2 0 0 1 -2.2 -2.2v-1a2.2 2.2 0 0 0 -.64 -1.55l-.7 -.7a2.2 2.2 0 0 1 0 -3.12l.7 -.7a2.2 2.2 0 0 0 .64 -1.55v-1' /></svg>"
            },
            {
                "Egg",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-egg' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M5.514 14.639c0 3.513 2.904 6.361 6.486 6.361s6.486 -2.848 6.486 -6.361a12.574 12.574 0 0 0 -3.243 -9.012a4.025 4.025 0 0 0 -3.243 -1.627a4.025 4.025 0 0 0 -3.243 1.627a12.574 12.574 0 0 0 -3.243 9.012' /></svg>"
            },
            {
                "Glass",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-glass' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <line x1='8' y1='21' x2='16' y2='21' />
  <line x1='12' y1='15' x2='12' y2='21' />
  <path d='M17 3l1 7c0 3.012 -2.686 5 -6 5s-6 -1.988 -6 -5l1 -7h10z' /></svg>"
            },
            {
                "Lemon",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-lemon' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M17.536 3.393c3.905 3.906 3.905 10.237 0 14.143c-3.906 3.905 -10.237 3.905 -14.143 0l14.143 -14.143' />
  <path d='M5.868 15.06a6.5 6.5 0 0 0 9.193 -9.192' />
  <path d='M10.464 10.464l4.597 4.597' />
  <path d='M10.464 10.464v6.364' />
  <path d='M10.464 10.464h6.364' /></svg>"
            },
            {
                "Lego",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-lego' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <line x1='9.5' y1='11' x2='9.51' y2='11' />
  <line x1='14.5' y1='11' x2='14.51' y2='11' />
  <path d='M9.5 15a3.5 3.5 0 0 0 5 0' />
  <path d='M7 5h1v-2h8v2h1a3 3 0 0 1 3 3v9a3 3 0 0 1 -3 3v1h-10v-1a3 3 0 0 1 -3 -3v-9a3 3 0 0 1 3 -3' /></svg>"
            },
            {
                "Leaf",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-leaf' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M5 21c.5 -4.5 2.5 -8 7 -10' />
  <path d='M9 18c6.218 0 10.5 -3.288 11 -12v-2h-4.014c-9 0 -11.986 4 -12 9c0 1 0 3 2 5h3z' /></svg>"
            },
            {
                "Pill",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-pill' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M4.5 12.5l8 -8a4.94 4.94 0 0 1 7 7l-8 8a4.94 4.94 0 0 1 -7 -7' />
  <line x1='8.5' y1='8.5' x2='15.5' y2='15.5' /></svg>"
            },
            {
                "Report Money",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-report-money' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M9 5h-2a2 2 0 0 0 -2 2v12a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-12a2 2 0 0 0 -2 -2h-2' />
  <rect x='9' y='3' width='6' height='4' rx='2' />
  <path d='M14 11h-2.5a1.5 1.5 0 0 0 0 3h1a1.5 1.5 0 0 1 0 3h-2.5' />
  <path d='M12 17v1m0 -8v1' /></svg>"
            },
            {
                "T-Shirt",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-shirt' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M15 4l6 2v5h-3v8a1 1 0 0 1 -1 1h-10a1 1 0 0 1 -1 -1v-8h-3v-5l6 -2a3 3 0 0 0 6 0' /></svg>"
            },
            {
                "Tag",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-tag' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M11 3l9 9a1.5 1.5 0 0 1 0 2l-6 6a1.5 1.5 0 0 1 -2 0l-9 -9v-4a4 4 0 0 1 4 -4h4' />
  <circle cx='9' cy='9' r='2' /></svg>"
            },
            {
                "Dollar",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-currency-dollar' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M16.7 8a3 3 0 0 0 -2.7 -2h-4a3 3 0 0 0 0 6h4a3 3 0 0 1 0 6h-4a3 3 0 0 1 -2.7 -2' />
  <path d='M12 3v3m0 12v3' /></svg>"
            },
            {
                "Euro",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-currency-euro' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M17.2 7a6 7 0 1 0 0 10' />
  <path d='M13 10h-8m0 4h8' /></svg>"
            },
            {
                "BitCoin",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-currency-bitcoin' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M6 6h8a3 3 0 0 1 0 6a3 3 0 0 1 0 6h-8' />
  <line x1='8' y1='6' x2='8' y2='18' />
  <line x1='8' y1='12' x2='14' y2='12' />
  <line x1='9' y1='3' x2='9' y2='6' />
  <line x1='13' y1='3' x2='13' y2='6' />
  <line x1='9' y1='18' x2='9' y2='21' />
  <line x1='13' y1='18' x2='13' y2='21' /></svg>"
            },
            {
                "Rupee",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-currency-rupee' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M18 5h-11h3a4 4 0 0 1 0 8h-3l6 6' />
  <line x1='7' y1='9' x2='18' y2='9' /></svg>"
            },
            {
                "Yen",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-currency-yen' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M12 19v-7l-5 -7m10 0l-5 7' />
  <line x1='8' y1='17' x2='16' y2='17' />
  <line x1='8' y1='13' x2='16' y2='13' /></svg>"
            },
            {
                "Gift",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-gift' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <rect x='3' y='8' width='18' height='4' rx='1' />
  <line x1='12' y1='8' x2='12' y2='21' />
  <path d='M19 12v7a2 2 0 0 1 -2 2h-10a2 2 0 0 1 -2 -2v-7' />
  <path d='M7.5 8a2.5 2.5 0 0 1 0 -5a4.8 8 0 0 1 4.5 5a4.8 8 0 0 1 4.5 -5a2.5 2.5 0 0 1 0 5' /></svg>"
            },
            {
                "Heart",
                @"<svg xmlns='http://www.w3.org/2000/svg' class='icon icon-tabler icon-tabler-heart' width='44' height='44' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor' fill='none' stroke-linecap='round' stroke-linejoin='round'>
  <path stroke='none' d='M0 0h24v24H0z' fill='none'/>
  <path d='M19.5 13.572l-7.5 7.428l-7.5 -7.428m0 0a5 5 0 1 1 7.5 -6.566a5 5 0 1 1 7.5 6.572' /></svg>"
            }
        };
    }
}
<script>
    let isFilterOpen = $state(false);
</script>

<div class="search-container">
    <div class="search-input-container">
        <input type="search" class="search-input" placeholder="Search...">
        <button
                type="button"
                class="filter-button"
                on:click={() => isFilterOpen = !isFilterOpen}>
            <img src="/images/filterIcon.svg"/>
        </button>
    </div>
    {#if isFilterOpen}
        <div class="filter-menu">
            <details>
                <summary>Material</summary>
                
                <br/>
                <ul style="list-style-type: none; padding: 0; margin: 0;">
                    <li><label><input type="checkbox" /> Quartz</label></li>
                    <li><label><input type="checkbox" /> Amethyst</label></li>
                </ul>
            </details>
        </div>
    {/if}
    <button type="submit" class="search-btn">Search</button>
</div>

<p class="results-title">Results</p>
<div class="results">
    <a class="item" href="/bracelet/1">
        <img class="thumbnail" src="https://le-bijouparis.com/cdn/shop/files/A65FC327-6EE9-4B58-8954-3984ED1A802E.jpg?v=1769290043&width=1206"/>
        <h2>Silk Bracelet</h2>
        <p class="description">Red Amethyst</p>
    </a>
    <a class="item" href="/bracelet/1">
        <img class="thumbnail" src="https://le-bijouparis.com/cdn/shop/files/A65FC327-6EE9-4B58-8954-3984ED1A802E.jpg?v=1769290043&width=1206"/>
        <h2>Very Complicated Fancy Bracelet</h2>
        <p class="description">Rose Quartz, Amethyst, Lava Stone, Glass</p>
    </a>
    <a class="item" href="/bracelet/1">
        <img class="thumbnail" src="https://le-bijouparis.com/cdn/shop/files/A65FC327-6EE9-4B58-8954-3984ED1A802E.jpg?v=1769290043&width=1206"/>
        <h2>Simple Bracelet</h2>
        <p class="description">String</p>
    </a>
    <a class="item" href="/bracelet/1">
        <img class="thumbnail" src="https://le-bijouparis.com/cdn/shop/files/A65FC327-6EE9-4B58-8954-3984ED1A802E.jpg?v=1769290043&width=1206"/>
        <h2>Interesting Bracelet</h2>
        <p class="description">Secrets</p>
    </a>
    <a class="item" href="/bracelet/1">
        <img class="thumbnail" src="https://le-bijouparis.com/cdn/shop/files/A65FC327-6EE9-4B58-8954-3984ED1A802E.jpg?v=1769290043&width=1206"/>
        <h2>Interesting Bracelet</h2>
        <p class="description">Secrets</p>
    </a>
    <a class="item" href="/bracelet/1">
        <img class="thumbnail" src="https://le-bijouparis.com/cdn/shop/files/A65FC327-6EE9-4B58-8954-3984ED1A802E.jpg?v=1769290043&width=1206"/>
        <h2>Interesting Bracelet</h2>
        <p class="description">Secrets</p>
    </a>
</div>

<style>
    .search-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 20px;
        width: 100%;
        max-width: 400px;
        margin: 0 auto 50px;
    }
    
    .search-input-container {
        display: flex;
        flex-direction: row;
        width: 100%;
        justify-content: space-between;
    }

    .filter-button {
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 10px 10px;
        background-color: #ffffff;
        border: 1px solid #666666;
        border-radius: 8px;
        cursor: pointer;
    }

    .filter-button img {
        width: 18px;
        height: 18px;
    }

    .filter-menu {
        width: 100%;
        padding: 12px;
        background-color: #f9f9f9;
        border: 1px solid #e0e0e0;
        border-radius: 8px;
        box-sizing: border-box;
        display: flex;
        flex-direction: column;
        gap: 8px;
    }

    .search-input {
        flex: 1;
        background-color: #ffffff;
        color: #1a1a1a;
        font-size: 1rem;
        padding: 10px 16px;
        margin-right: 15px;
        border: 1px solid #666666;
        border-radius: 8px;
        outline: none;
    }

    .search-input::placeholder {
        color: #999999;
    }

    .search-input:focus {
        border-color: #aaaaaa;
    }

    .search-btn {
        background-color: #ffffff;
        color: #050505;
        border: #050505 1px solid;
        font-size: 1rem;
        font-weight: 500;
        padding: 10px 15px;
        border-radius: 8px;
        cursor: pointer;
        width: 100%;
    }

    .search-btn:hover {
        background-color: #f0f0f0;
    }
    
    .results-title {
        margin: 10px 0 50px;
    }
    
    .results {
        display: flex;
        flex-wrap: wrap;
        justify-content: safe center;
        padding: 0 0 15px;
        gap: 10px;
    }

    .results .item {
        all: unset;
        cursor: pointer;
        display: flex;
        flex: 0 0 150px;
        flex-direction: column;
        border: #808080 1px solid;
        justify-content: start;
        align-items: center;
        padding: 10px;
        width: 150px;
    }
    
    .results .item h2 {
        font-size: 0.9rem;
        font-weight: 600;
        margin-top: 1rem;
        text-align: left;
        align-self: start;
    }

    .results .item .thumbnail,
    .results .item .thumbnail img {
        width: 100%;
        max-width: 100%;
        height: auto;
        display: block;
        object-fit: cover;
    }

    .results .item .description {
        font-size: 0.6rem;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        align-self: start;
        margin-top: 0;
        width: 100%;
        color: #696969;
    }

    @media(min-width: 1024px) {
        .results-title {
            text-align: center;
        }
        
        .results .item {
            flex: 0 0 300px;
            width: 300px;
        }
    }
</style>
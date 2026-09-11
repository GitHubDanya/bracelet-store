<script>
</script>

<svelte:head>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=IBM+Plex+Sans+Devanagari:wght@100;200;300;400;500;600;700&display=swap" rel="stylesheet">
</svelte:head>

<div class="title">
    Elegant, handmade accessories
</div>

<p class="store-row-title">Bracelets</p>
<div class="store-row">
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
</div>

<p class="store-row-title">New Items</p>
<div class="store-row">
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

<div class="tail">
    © 2026
</div>

<style>
    .title {
        display: flex;
        justify-content: center;
        align-items: center;
        font-weight: 200;
        font-size: 3rem;
        text-align: center;
        height: calc(100vh - 100px);
    }
    
    @media(min-width: 1024px) {
        .title {
            margin: 300px 0;
            height: auto;
        }
    }
    
    .store-row-title {
        margin-top: 10vh;
    }
    
    .store-row {
        display: flex;
        overflow-x: auto;
        justify-content: safe center;
        padding: 0 0 15px;
        gap: 10px;
    }
    
    @media(min-width: 800px) {
        .store-row-title {
            text-align: center;
        }
    }
    
    .store-row .item {
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
    
    @media(min-width: 1024px) {
        .store-row .item {
            flex: 0 0 300px;
            width: 300px;
        }
    }
    
    .store-row .item h2 {
        font-size: 0.9rem;
        font-weight: 600;
        margin-top: 1rem;
        text-align: left;
        align-self: start;
    }
    
    .store-row .item .thumbnail,
    .store-row .item .thumbnail img {
        width: 100%;
        max-width: 100%;
        height: auto;
        display: block;
        object-fit: cover;
    }
    
    .store-row .item .description {
        font-size: 0.6rem;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        align-self: start;
        margin-top: 0;
        width: 100%;
        color: #696969;
    }
    
    .tail {
        height: 10vh;
        margin: 25vh 0 0;
        display: flex;
        justify-content: center;
        align-items: center;
        text-align: center;
        color: #696969;
        font-size: 0.6rem;
    }
</style>
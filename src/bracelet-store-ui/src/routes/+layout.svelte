<script lang="ts">
	import favicon from '$lib/assets/favicon.svg';

	let { children } = $props();
</script>

<svelte:head>
	<link rel="preconnect" href="https://fonts.googleapis.com">
	<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
	<link href="https://fonts.googleapis.com/css2?family=PT+Serif+Caption:ital@0;1&display=swap" rel="stylesheet">
	
	<link rel="icon" href={favicon} />
</svelte:head>

<header class="top-bar">
	<div class="logo">
		<a href="/">Logo</a>
	</div>
	<nav>
		<a href="/store">Bracelets</a>
		<a href="/about">About</a>
	</nav>
</header>

<main class="container">
	{@render children()}
</main>

<div class="tail">
	© 2026
</div>

<style>
	:global(body) {
		font-family: "PT Serif Caption", serif;
		background-color: #ffffff;
	}
	
	.top-bar {
		display: flex;
		justify-content: left;
		align-items: center;
		padding: 1rem 2rem;
		background-color: #ffffff;
		color: #111111;
	}
	
	@media(max-width: 1024px) {
		.top-bar {
			justify-content: center;
		}
	}

	.logo a {
		font-weight: bold;
		font-size: 1.25rem;
		color: #111111;
		text-decoration: none;
	}

	nav a {
		color: #111111;
		margin-left: 1.5rem;
		text-decoration: none;
	}

	nav a:hover {
		color: #ffffff;
	}

	.container {
		padding: 2rem;
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
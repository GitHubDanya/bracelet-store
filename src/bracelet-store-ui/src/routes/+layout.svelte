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
		<a class="logo" href="/">
			<p>Cherry </p>
			<img src="/images/cherryIcon.svg"/>
		</a>
	</div>
	<nav>
		<a href="/bracelets">Bracelets</a>
		<a href="/about">About</a>
	</nav>
	<div class="spacer"></div>
	<button class="language-button">
		<img class="language-icon" src="/images/languageIcon.svg"/>
	</button>
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
		justify-content: center;
		align-items: center;
		padding: 1rem 2rem;
		background-color: #ffffff;
		color: #111111;
	}
	
	.logo {
		display: flex;
		flex-direction: row;
		justify-content: center;
		align-items: center;
		gap: 1rem;
		margin-right: 1rem;
	}

	.logo a {
		font-weight: bold;
		font-size: 1.25rem;
		color: #111111;
		text-decoration: none;
	}
	
	.logo a img {
		height: 2rem;
		width: 2rem;
	}
	
	nav {
		display: flex;
		flex-direction: row;
		justify-content: center;
		align-items: center;
	}

	nav a {
		color: #111111;
		margin-left: 1.5rem;
		text-decoration: none;
	}

	nav a:hover {
		color: #999999;
	}
	
	.spacer {
		width: 2rem;
	}

	.language-button {
		all: unset;
		cursor: pointer;
		display: flex;
		align-items: center;
		justify-content: center;
	}
	
	.language-button .language-icon {
		height: 1rem;
		width: 1rem;
	}

	.container {
		padding: 2rem;
	}

	.tail {
		height: 10vh;
		display: flex;
		justify-content: center;
		align-items: center;
		text-align: center;
		color: #696969;
		font-size: 0.6rem;
	}

	@media(min-width: 1024px) {
		.top-bar {
			justify-content: left;
		}

		.spacer {
			width: 100%;
		}
		
		.language-button .language-icon {
			width: 1.8rem;
			height: 1.8rem;
		}
	}
</style>
<script context="module">
  import { readable } from "svelte/store";
  export const depts = readable();
</script>

<script>
  import Layout from "../../Shared/Layout.svelte";
  import Common from "./Common.svelte";
  import Private from "./Private.svelte";
  import Access from "./Access.svelte";
  import Education from "./Education.svelte";
  import Hist from "./Hist.svelte";
  import Photo from "./Photo.svelte";
  import page from "page";
  import { route } from "../../store.js";
  import { onMount, onDestroy } from "svelte";
  import { asLoader } from "../../Shared/Actions.js";

  $: molId = $route.params.id;

  let mol = null;
  let depts = [];
  let posts = [];

  let loading = true;

  onMount(async x => {
    loading = true;
    let q = await fetch(`/api/mols/${$route.params.id}`);
    mol = await q.json();

    q = await fetch(`/api/mols/depts`);
    depts = await q.json();

    q = await fetch(`/api/mols/posts`);
    posts = await q.json();
    loading = false;
  });

  let comp = Common;

  let save = async x => {
    loading = true;

    try {
      let q = await fetch(`/api/mols/${x.MOL_ID}`, {
        method: "post",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(x)
      });
      mol = await q.json();
    } catch (err) {
      console.error(err);
    }

    loading = false;
  };

  // onMount(async x => {
  //   let r = await fetch(`/api/mols/${molId}`);
  //   mol = await r.json();
  // });
</script>

<Layout>



  {#if mol}
    <div class="container">



      <nav class="navbar navbar-expand-lg navbar-light bg-light mb-3 shadow-sm">
        <div class="navbar-brand mr-auto">{mol.SURNAME} {mol.NAME1} {mol.NAME2}</div>

        <ul class="navbar-nav">
          <li class="nav-item" class:active={comp === Common}>
            <a
              href="/"
              class="nav-link"
              on:click|preventDefault={x => (comp = Common)}>
              Основные
            </a>
          </li>
          <li class="nav-item" class:active={comp === Private}>
            <a
              href="/"
              class="nav-link"
              on:click|preventDefault={x => (comp = Private)}>
              Личные
            </a>

          </li>
          <li class="nav-item" class:active={comp === Photo}>
            <a
              href="/"
              class="nav-link"
              on:click|preventDefault={x => (comp = Photo)}>
              Фото
            </a>
          </li>

          <li class="nav-item" class:active={comp === Access}>
            <a
              href="/"
              class="nav-item nav-link"
              on:click|preventDefault={x => (comp = Access)}>
              КИСП
              {#if mol.NAME_LOGON}
                <i class="fa fa-check" />
              {:else}
                <i class="fa fa-remove" />
              {/if}
            </a>

          </li>

          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              href="/"
              role="button"
              data-toggle="dropdown">
              Дополнительно
            </a>
            <div class="dropdown-menu">
              <a class="dropdown-item" href="/" on:click|preventDefault={x => comp = Hist}>История назначений</a>
              <a class="dropdown-item" href="/" on:click|preventDefault={x => comp = Education}>Обучение</a>
            </div>
          </li>
        </ul>


      </nav>

      <div class="row">
        <div class="col-3">

          <div>
            {#if mol.PHOTO_IMAGE}
              <img
                width="100%"
                class="shadow p-3 mb-5 bg-white rounded"
                src="data:image/jpeg;base64, {mol.PHOTO_IMAGE}"
                alt="" />
            {:else}
              <img
                width="100%"
                class="shadow p-3 mb-5 bg-white rounded"
                src="/No-photo-m.png"
                alt="" />

            {/if}

          </div>

        </div>
        <div class="col-9">

          <div class="card shadow">
            <div class="card-body">

              <svelte:component
                this={comp}
                mol={{...mol}}
                {depts}
                {posts}
                {save}
                {loading} />

            </div>
          </div>

        </div>
      </div>

    </div>
  {/if}


</Layout>

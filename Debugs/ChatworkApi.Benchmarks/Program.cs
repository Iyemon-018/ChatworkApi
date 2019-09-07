using System;

namespace ChatworkApi.Benchmarks
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;

    public enum BenchmarkType
    {
        [Alias("Value1")] Value1

      , [Alias("Value2")] Value2

      , [Alias("Value3")] Value3

      , [Alias("Value4")] Value4

      , [Alias("Value5")] Value5

      , [Alias("Value6")] Value6

      , [Alias("Value7")] Value7

      , [Alias("Value8")] Value8

      , [Alias("Value9")] Value9

      , [Alias("Value10")] Value10

      , [Alias("Value11")] Value11

      , [Alias("Value12")] Value12

      , [Alias("Value13")] Value13

      , [Alias("Value14")] Value14

      , [Alias("Value15")] Value15

      , [Alias("Value16")] Value16

      , [Alias("Value17")] Value17

      , [Alias("Value18")] Value18

      , [Alias("Value19")] Value19

      , [Alias("Value20")] Value20

      , [Alias("Value21")] Value21

      , [Alias("Value22")] Value22

      , [Alias("Value23")] Value23

      , [Alias("Value24")] Value24

      , [Alias("Value25")] Value25

      , [Alias("Value26")] Value26

      , [Alias("Value27")] Value27

      , [Alias("Value28")] Value28

      , [Alias("Value29")] Value29

      , [Alias("Value30")] Value30

      , [Alias("Value31")] Value31

      , [Alias("Value32")] Value32

      , [Alias("Value33")] Value33

      , [Alias("Value34")] Value34

      , [Alias("Value35")] Value35

      , [Alias("Value36")] Value36

      , [Alias("Value37")] Value37

      , [Alias("Value38")] Value38

      , [Alias("Value39")] Value39

      , [Alias("Value40")] Value40

      , [Alias("Value41")] Value41

      , [Alias("Value42")] Value42

      , [Alias("Value43")] Value43

      , [Alias("Value44")] Value44

      , [Alias("Value45")] Value45

      , [Alias("Value46")] Value46

      , [Alias("Value47")] Value47

      , [Alias("Value48")] Value48

      , [Alias("Value49")] Value49

      , [Alias("Value50")] Value50

      , [Alias("Value51")] Value51

      , [Alias("Value52")] Value52

      , [Alias("Value53")] Value53

      , [Alias("Value54")] Value54

      , [Alias("Value55")] Value55

      , [Alias("Value56")] Value56

      , [Alias("Value57")] Value57

      , [Alias("Value58")] Value58

      , [Alias("Value59")] Value59

      , [Alias("Value60")] Value60

      , [Alias("Value61")] Value61

      , [Alias("Value62")] Value62

      , [Alias("Value63")] Value63

      , [Alias("Value64")] Value64

      , [Alias("Value65")] Value65

      , [Alias("Value66")] Value66

      , [Alias("Value67")] Value67

      , [Alias("Value68")] Value68

      , [Alias("Value69")] Value69

      , [Alias("Value70")] Value70

      , [Alias("Value71")] Value71

      , [Alias("Value72")] Value72

      , [Alias("Value73")] Value73

      , [Alias("Value74")] Value74

      , [Alias("Value75")] Value75

      , [Alias("Value76")] Value76

      , [Alias("Value77")] Value77

      , [Alias("Value78")] Value78

      , [Alias("Value79")] Value79

      , [Alias("Value80")] Value80

      , [Alias("Value81")] Value81

      , [Alias("Value82")] Value82

      , [Alias("Value83")] Value83

      , [Alias("Value84")] Value84

      , [Alias("Value85")] Value85

      , [Alias("Value86")] Value86

      , [Alias("Value87")] Value87

      , [Alias("Value88")] Value88

      , [Alias("Value89")] Value89

      , [Alias("Value90")] Value90

      , [Alias("Value91")] Value91

      , [Alias("Value92")] Value92

      , [Alias("Value93")] Value93

      , [Alias("Value94")] Value94

      , [Alias("Value95")] Value95

      , [Alias("Value96")] Value96

      , [Alias("Value97")] Value97

      , [Alias("Value98")] Value98

      , [Alias("Value99")] Value99

      , [Alias("Value100")] Value100

      , [Alias("Value101")] Value101

      , [Alias("Value102")] Value102

      , [Alias("Value103")] Value103

      , [Alias("Value104")] Value104

      , [Alias("Value105")] Value105

      , [Alias("Value106")] Value106

      , [Alias("Value107")] Value107

      , [Alias("Value108")] Value108

      , [Alias("Value109")] Value109

      , [Alias("Value110")] Value110

      , [Alias("Value111")] Value111

      , [Alias("Value112")] Value112

      , [Alias("Value113")] Value113

      , [Alias("Value114")] Value114

      , [Alias("Value115")] Value115

      , [Alias("Value116")] Value116

      , [Alias("Value117")] Value117

      , [Alias("Value118")] Value118

      , [Alias("Value119")] Value119

      , [Alias("Value120")] Value120

      , [Alias("Value121")] Value121

      , [Alias("Value122")] Value122

      , [Alias("Value123")] Value123

      , [Alias("Value124")] Value124

      , [Alias("Value125")] Value125

      , [Alias("Value126")] Value126

      , [Alias("Value127")] Value127

      , [Alias("Value128")] Value128

      , [Alias("Value129")] Value129

      , [Alias("Value130")] Value130

      , [Alias("Value131")] Value131

      , [Alias("Value132")] Value132

      , [Alias("Value133")] Value133

      , [Alias("Value134")] Value134

      , [Alias("Value135")] Value135

      , [Alias("Value136")] Value136

      , [Alias("Value137")] Value137

      , [Alias("Value138")] Value138

      , [Alias("Value139")] Value139

      , [Alias("Value140")] Value140

      , [Alias("Value141")] Value141

      , [Alias("Value142")] Value142

      , [Alias("Value143")] Value143

      , [Alias("Value144")] Value144

      , [Alias("Value145")] Value145

      , [Alias("Value146")] Value146

      , [Alias("Value147")] Value147

      , [Alias("Value148")] Value148

      , [Alias("Value149")] Value149

      , [Alias("Value150")] Value150

      , [Alias("Value151")] Value151

      , [Alias("Value152")] Value152

      , [Alias("Value153")] Value153

      , [Alias("Value154")] Value154

      , [Alias("Value155")] Value155

      , [Alias("Value156")] Value156

      , [Alias("Value157")] Value157

      , [Alias("Value158")] Value158

      , [Alias("Value159")] Value159

      , [Alias("Value160")] Value160

      , [Alias("Value161")] Value161

      , [Alias("Value162")] Value162

      , [Alias("Value163")] Value163

      , [Alias("Value164")] Value164

      , [Alias("Value165")] Value165

      , [Alias("Value166")] Value166

      , [Alias("Value167")] Value167

      , [Alias("Value168")] Value168

      , [Alias("Value169")] Value169

      , [Alias("Value170")] Value170

      , [Alias("Value171")] Value171

      , [Alias("Value172")] Value172

      , [Alias("Value173")] Value173

      , [Alias("Value174")] Value174

      , [Alias("Value175")] Value175

      , [Alias("Value176")] Value176

      , [Alias("Value177")] Value177

      , [Alias("Value178")] Value178

      , [Alias("Value179")] Value179

      , [Alias("Value180")] Value180

      , [Alias("Value181")] Value181

      , [Alias("Value182")] Value182

      , [Alias("Value183")] Value183

      , [Alias("Value184")] Value184

      , [Alias("Value185")] Value185

      , [Alias("Value186")] Value186

      , [Alias("Value187")] Value187

      , [Alias("Value188")] Value188

      , [Alias("Value189")] Value189

      , [Alias("Value190")] Value190

      , [Alias("Value191")] Value191

      , [Alias("Value192")] Value192

      , [Alias("Value193")] Value193

      , [Alias("Value194")] Value194

      , [Alias("Value195")] Value195

      , [Alias("Value196")] Value196

      , [Alias("Value197")] Value197

      , [Alias("Value198")] Value198

      , [Alias("Value199")] Value199

      , [Alias("Value200")] Value200

      , [Alias("Value201")] Value201

      , [Alias("Value202")] Value202

      , [Alias("Value203")] Value203

      , [Alias("Value204")] Value204

      , [Alias("Value205")] Value205

      , [Alias("Value206")] Value206

      , [Alias("Value207")] Value207

      , [Alias("Value208")] Value208

      , [Alias("Value209")] Value209

      , [Alias("Value210")] Value210

      , [Alias("Value211")] Value211

      , [Alias("Value212")] Value212

      , [Alias("Value213")] Value213

      , [Alias("Value214")] Value214

      , [Alias("Value215")] Value215

      , [Alias("Value216")] Value216

      , [Alias("Value217")] Value217

      , [Alias("Value218")] Value218

      , [Alias("Value219")] Value219

      , [Alias("Value220")] Value220

      , [Alias("Value221")] Value221

      , [Alias("Value222")] Value222

      , [Alias("Value223")] Value223

      , [Alias("Value224")] Value224

      , [Alias("Value225")] Value225

      , [Alias("Value226")] Value226

      , [Alias("Value227")] Value227

      , [Alias("Value228")] Value228

      , [Alias("Value229")] Value229

      , [Alias("Value230")] Value230

      , [Alias("Value231")] Value231

      , [Alias("Value232")] Value232

      , [Alias("Value233")] Value233

      , [Alias("Value234")] Value234

      , [Alias("Value235")] Value235

      , [Alias("Value236")] Value236

      , [Alias("Value237")] Value237

      , [Alias("Value238")] Value238

      , [Alias("Value239")] Value239

      , [Alias("Value240")] Value240

      , [Alias("Value241")] Value241

      , [Alias("Value242")] Value242

      , [Alias("Value243")] Value243

      , [Alias("Value244")] Value244

      , [Alias("Value245")] Value245

      , [Alias("Value246")] Value246

      , [Alias("Value247")] Value247

      , [Alias("Value248")] Value248

      , [Alias("Value249")] Value249

      , [Alias("Value250")] Value250

      , [Alias("Value251")] Value251

      , [Alias("Value252")] Value252

      , [Alias("Value253")] Value253

      , [Alias("Value254")] Value254

      , [Alias("Value255")] Value255

      , [Alias("Value256")] Value256

      , [Alias("Value257")] Value257

      , [Alias("Value258")] Value258

      , [Alias("Value259")] Value259

      , [Alias("Value260")] Value260

      , [Alias("Value261")] Value261

      , [Alias("Value262")] Value262

      , [Alias("Value263")] Value263

      , [Alias("Value264")] Value264

      , [Alias("Value265")] Value265

      , [Alias("Value266")] Value266

      , [Alias("Value267")] Value267

      , [Alias("Value268")] Value268

      , [Alias("Value269")] Value269

      , [Alias("Value270")] Value270

      , [Alias("Value271")] Value271

      , [Alias("Value272")] Value272

      , [Alias("Value273")] Value273

      , [Alias("Value274")] Value274

      , [Alias("Value275")] Value275

      , [Alias("Value276")] Value276

      , [Alias("Value277")] Value277

      , [Alias("Value278")] Value278

      , [Alias("Value279")] Value279

      , [Alias("Value280")] Value280

      , [Alias("Value281")] Value281

      , [Alias("Value282")] Value282

      , [Alias("Value283")] Value283

      , [Alias("Value284")] Value284

      , [Alias("Value285")] Value285

      , [Alias("Value286")] Value286

      , [Alias("Value287")] Value287

      , [Alias("Value288")] Value288

      , [Alias("Value289")] Value289

      , [Alias("Value290")] Value290

      , [Alias("Value291")] Value291

      , [Alias("Value292")] Value292

      , [Alias("Value293")] Value293

      , [Alias("Value294")] Value294

      , [Alias("Value295")] Value295

      , [Alias("Value296")] Value296

      , [Alias("Value297")] Value297

      , [Alias("Value298")] Value298

      , [Alias("Value299")] Value299

      , [Alias("Value300")] Value300

      , [Alias("Value301")] Value301

      , [Alias("Value302")] Value302

      , [Alias("Value303")] Value303

      , [Alias("Value304")] Value304

      , [Alias("Value305")] Value305

      , [Alias("Value306")] Value306

      , [Alias("Value307")] Value307

      , [Alias("Value308")] Value308

      , [Alias("Value309")] Value309

      , [Alias("Value310")] Value310

      , [Alias("Value311")] Value311

      , [Alias("Value312")] Value312

      , [Alias("Value313")] Value313

      , [Alias("Value314")] Value314

      , [Alias("Value315")] Value315

      , [Alias("Value316")] Value316

      , [Alias("Value317")] Value317

      , [Alias("Value318")] Value318

      , [Alias("Value319")] Value319

      , [Alias("Value320")] Value320

      , [Alias("Value321")] Value321

      , [Alias("Value322")] Value322

      , [Alias("Value323")] Value323

      , [Alias("Value324")] Value324

      , [Alias("Value325")] Value325

      , [Alias("Value326")] Value326

      , [Alias("Value327")] Value327

      , [Alias("Value328")] Value328

      , [Alias("Value329")] Value329

      , [Alias("Value330")] Value330

      , [Alias("Value331")] Value331

      , [Alias("Value332")] Value332

      , [Alias("Value333")] Value333

      , [Alias("Value334")] Value334

      , [Alias("Value335")] Value335

      , [Alias("Value336")] Value336

      , [Alias("Value337")] Value337

      , [Alias("Value338")] Value338

      , [Alias("Value339")] Value339

      , [Alias("Value340")] Value340

      , [Alias("Value341")] Value341

      , [Alias("Value342")] Value342

      , [Alias("Value343")] Value343

      , [Alias("Value344")] Value344

      , [Alias("Value345")] Value345

      , [Alias("Value346")] Value346

      , [Alias("Value347")] Value347

      , [Alias("Value348")] Value348

      , [Alias("Value349")] Value349

      , [Alias("Value350")] Value350

      , [Alias("Value351")] Value351

      , [Alias("Value352")] Value352

      , [Alias("Value353")] Value353

      , [Alias("Value354")] Value354

      , [Alias("Value355")] Value355

      , [Alias("Value356")] Value356

      , [Alias("Value357")] Value357

      , [Alias("Value358")] Value358

      , [Alias("Value359")] Value359

      , [Alias("Value360")] Value360

      , [Alias("Value361")] Value361

      , [Alias("Value362")] Value362

      , [Alias("Value363")] Value363

      , [Alias("Value364")] Value364

      , [Alias("Value365")] Value365

      , [Alias("Value366")] Value366

      , [Alias("Value367")] Value367

      , [Alias("Value368")] Value368

      , [Alias("Value369")] Value369

      , [Alias("Value370")] Value370

      , [Alias("Value371")] Value371

      , [Alias("Value372")] Value372

      , [Alias("Value373")] Value373

      , [Alias("Value374")] Value374

      , [Alias("Value375")] Value375

      , [Alias("Value376")] Value376

      , [Alias("Value377")] Value377

      , [Alias("Value378")] Value378

      , [Alias("Value379")] Value379

      , [Alias("Value380")] Value380

      , [Alias("Value381")] Value381

      , [Alias("Value382")] Value382

      , [Alias("Value383")] Value383

      , [Alias("Value384")] Value384

      , [Alias("Value385")] Value385

      , [Alias("Value386")] Value386

      , [Alias("Value387")] Value387

      , [Alias("Value388")] Value388

      , [Alias("Value389")] Value389

      , [Alias("Value390")] Value390

      , [Alias("Value391")] Value391

      , [Alias("Value392")] Value392

      , [Alias("Value393")] Value393

      , [Alias("Value394")] Value394

      , [Alias("Value395")] Value395

      , [Alias("Value396")] Value396

      , [Alias("Value397")] Value397

      , [Alias("Value398")] Value398

      , [Alias("Value399")] Value399

       ,

    }

    public class BenchmarkAlias
    {
        [Benchmark]
        public void Cache()
        {
            var values = Enum.GetValues(typeof(BenchmarkType)).OfType<BenchmarkType>().ToArray();
            foreach (var benchmarkType in values)
            {
                var value = benchmarkType.ToAlias();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var result = BenchmarkRunner.Run<BenchmarkAlias>();
        }
    }
}
